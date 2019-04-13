using ATM.Infrastructure.Entities;
using ATM.Infrastructure.ViewModels;
using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ATM.Infrastructure.Services
{
    public class CustomerService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<int> AddCustomer(RegisterCustomerViewModel model, string UserId)
        {
            Customer customer = new Customer
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Address = model.Address,
                UserId = UserId
            };
            db.Customer.Add(customer);
            await db.SaveChangesAsync();
            return customer.ID;
        }


        public async Task CreateCustomerAccount(int customerId, decimal balance = 1000)
        {
            string accountNumber =  GenerateAccountNumber("ATM");
            Account userAccount = new Account
            {
                CustomerId = customerId,
                Balance = balance,
                Id = accountNumber
            };
            db.Account.Add(userAccount);
            await db.SaveChangesAsync();
        }

        private string GenerateAccountNumber(string format)
        {
            var lastAccountNumber =   db.Account.ToList().LastOrDefault();
            string newAccountNumber = string.Empty;
            if(lastAccountNumber != null)
            {
                newAccountNumber = format + DateTime.Now.Year +  (lastAccountNumber.Count += 1);
            }
            else
            {
                newAccountNumber = format + DateTime.Now.Year+  001;
            }
            return newAccountNumber;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await Task.Factory.StartNew(() => db.Customer.ToList());
        }
    }
}