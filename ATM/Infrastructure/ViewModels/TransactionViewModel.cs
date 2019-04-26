using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Infrastructure.ViewModels
{
    public class TransferFundViewModel
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}