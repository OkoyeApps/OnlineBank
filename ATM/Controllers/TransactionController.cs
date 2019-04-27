using ATM.Infrastructure.Entities;
using ATM.Infrastructure.Services;
using ATM.Infrastructure.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        TransactionService TransactionService = new TransactionService();
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Transfer(TransferFundViewModel fundModel)
        {
            Account SenderAccountDetail = await TransactionService.GetCustomerAccountDetails(User.Identity.GetUserId());
            if (SenderAccountDetail.Balance >= 100 && SenderAccountDetail.Balance - fundModel.Amount >= 100)
            {
                Account RecieverAccountDetail = await TransactionService.GetCustomerAccountDetails(null, fundModel.AccountNumber);
                await TransactionService.TransferFunds(SenderAccountDetail, RecieverAccountDetail, fundModel.Amount);
                TempData["message"] = new TempdataClass { message = "Transfer Successful", Route = Request.Url.AbsolutePath, messageType = messageType.Success };
                return RedirectToAction("Index", "Home");
            }
            TempData["message"] = new TempdataClass { message = "Insufficient balance", Route = Request.Url.AbsolutePath, messageType = messageType.Error };
            return View();
        }

        [HttpGet]
        public ActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Withdraw(FormCollection collection)
        {
            int amount = int.Parse(collection["amount"]);            
            int amountAgain = int.Parse(Request.Form["amount"]);
            //Get the Customer details needed for the transaction to take place
            Account withdrwalAccountDetail = await TransactionService.GetCustomerAccountDetails(User.Identity.GetUserId());
            if(withdrwalAccountDetail != null)
            {
                //Save initial balance bfore update
                decimal currentBalance = withdrwalAccountDetail.Balance;

                //Update Account balance by deducting the amount from the account
                await TransactionService.UpdateAccountDetails(withdrwalAccountDetail, 
                    amount, Infrastructure.Enum.TrancationEnum.Withdrawal);

                //Add a new Transaction row for this withdrwal
                await TransactionService.SaveTransactionRecord(amount,
                    currentBalance, withdrwalAccountDetail.CustomerId, 
                    withdrwalAccountDetail.Id, Infrastructure.Enum.TrancationEnum.Withdrawal);
                
                //this just sends feedback message to the user, indicating the status of the transaction
                TempData["message"] = new TempdataClass { message = "withdrawal Successful",
                    Route = Request.Url.AbsolutePath, messageType = messageType.Success };
                return RedirectToAction("Index", "Home");
            }
            TempData["message"] = new TempdataClass { message = @"sorry we couldn't find your account details,
                kindly visit a branch closest to you for complain",
                Route = Request.Url.AbsolutePath, messageType = messageType.Success };
            return View();
        }

        public async Task<ActionResult> Accountstatement(int pageNumber = 0, int pageSize = 10)
        {
            var currentuserAccountDetail = await TransactionService.GetCustomerAccountDetails(User.Identity.GetUserId());
            IEnumerable<Transactions> UserTransactions = 
                await TransactionService.GetAllUserTransaction(currentuserAccountDetail.Id, 
                pageSize: pageSize,pageNumber:pageNumber);
            return View("Accountstatement", UserTransactions);
        }
    }
}