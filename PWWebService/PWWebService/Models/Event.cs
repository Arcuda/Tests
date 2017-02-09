using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWWebService
{
    public class Event
    {
        public DateTime EventDT { get; set; }
        public string Process { get; set; }
        public string Client { get; set; }
        public int Transfer { get; set; }
    }
}