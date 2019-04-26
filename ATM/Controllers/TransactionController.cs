using ATM.Infrastructure.Entities;
using ATM.Infrastructure.Services;
using ATM.Infrastructure.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
            Account RecieverAccountDetail = await TransactionService.GetCustomerAccountDetails(null, fundModel.AccountNumber);
            await TransactionService.TransferFunds(SenderAccountDetail, RecieverAccountDetail, fundModel.Amount);
            return RedirectToAction("Index", "Home");
        }
    }
}