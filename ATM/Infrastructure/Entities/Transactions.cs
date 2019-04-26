using ATM.Infrastructure.Enum;
using ATM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATM.Infrastructure.Entities
{
    public class Transactions
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal NewBalance { get; set; }
        public string UserId { get; set; }
        public int CustomerId { get; set; }
        public string AccountId { get; set; }     
        public TrancationEnum TransactionType { get; set; }
        public DateTime TimeOfTransaction { get; set; } = DateTime.Now;
        public Customer Customer { get; set; }
        public Account Account { get; set; }
        public ApplicationUser User { get; set; }
    }
}