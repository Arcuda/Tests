using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PWWServ_Client.Models
{
    public class Customer
    {
        [Display(Name="E-Mail")]
        public string Email { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }
        [Display(Name = "Password")]
        public string PassHesh { get; set; }
    }
}