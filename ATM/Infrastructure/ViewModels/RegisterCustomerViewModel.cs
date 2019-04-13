using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.Infrastructure.ViewModels
{
    public class RegisterCustomerViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide first name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Your home address is needed for verification"), StringLength(200, MinimumLength = 10)]
        public string Address { get; set; }
        public HttpPostedFileBase Passport { get; set; }
        [Required(ErrorMessage = "Please provide your email address")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "We promise not to give out your number, kindly provide them"), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}