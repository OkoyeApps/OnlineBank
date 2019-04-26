using ATM.Infrastructure.Entities;
using ATM.Infrastructure.Enum;
using ATM.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ATM.Infrastructure.Services
{
    public class TransactionService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //Get Sender Dertails
        //Get Reciever Details
        //Transfer the money

        public async Task<Account> GetCustomerAccountDetails(string userId,
            string accountNumber = "")
        {
            Account customerAccount = new Account();
            if (!string.IsNullOrEmpty(userId))
            {
                customerAccount = await Task.Factory.StartNew(() =>
               db.Account.Include("Customer")
               .Where(x => x.Customer.UserId == userId).FirstOrDefault());
            }
            else
            {
                customerAccount = await Task.Factory.StartNew(() => 
                db.Account.Where(x => x.Id.ToLower() == accountNumber.ToLower())
                .FirstOrDefault());
            }
            return customerAccount;
        }


        public async Task TransferFunds(Account SenderAccountDetails,
                Account RecieverAccountDetails, decimal AmountToSend)
        {
            decimal senderCurrentBalance = SenderAccountDetails.Balance;
            decimal recieverCurrentBalance = RecieverAccountDetails.Balance;
            await UpdateAccountDetails(SenderAccountDetails, AmountToSend, TrancationEnum.Transfer);
            await UpdateAccountDetails(RecieverAccountDetails, AmountToSend, TrancationEnum.Deposit);
            await SaveTransactionRecord(AmountToSend,
                                       senderCurrentBalance,
                                        SenderAccountDetails.CustomerId,
                                        SenderAccountDetails.Id,
                                        TrancationEnum.Transfer);
            await SaveTransactionRecord(AmountToSend,
                                        recieverCurrentBalance,
                                        RecieverAccountDetails.CustomerId,
                                        RecieverAccountDetails.Id,
                                        TrancationEnum.Deposit
                );

        }

        public async Task<bool> SaveTransactionRecord(decimal amount, decimal previousBalance,
            int customerId, string AccountId, TrancationEnum transactionType
            )
        {
            try
            {
                decimal newBalance = 0;
                if (transactionType == TrancationEnum.Deposit)
                {
                    newBalance = previousBalance + amount;
                }
                else
                {
                    newBalance = previousBalance - amount;
                }
                Transactions transaction = new Transactions
                {
                    AccountId = AccountId,
                    Amount = amount,
                    NewBalance = newBalance,
                    CustomerId = customerId,
                    TransactionType = transactionType,
                    PreviousBalance = previousBalance,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task UpdateAccountDetails(Account accountToUpdate, decimal amount,
                                                        TrancationEnum transactionType)
        {
            if (transactionType == TrancationEnum.Deposit)
            {
                accountToUpdate.Balance += amount;

            }
            else
            {
                accountToUpdate.Balance -= amount;
            }
            db.Entry(accountToUpdate).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}