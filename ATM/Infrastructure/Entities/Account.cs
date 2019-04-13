using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATM.Infrastructure.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Count { get; set; }
        public Customer Customer { get; set; }
    }
}