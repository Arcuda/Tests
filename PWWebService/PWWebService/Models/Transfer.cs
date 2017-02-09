using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWWebService
{
    public class Transfer
    {
        public int Id { get; set; }
        public DateTime WhenDT { get; set; }
        public double Summa { get; set; }
        public double Amount { get; set; }
        public string Client { get; set; }
        public string Notes { get; set; }
        public int Source { get; set; }
    }
}