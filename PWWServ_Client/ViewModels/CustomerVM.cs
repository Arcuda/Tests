using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PWWServ_Client.Models;

namespace PWWServ_Client.ViewModels
{
    public class CustomerVM
    {
        public Customer Customer { get; set; }
        public List<Transfer> Transfers { get; set; }
    }
}