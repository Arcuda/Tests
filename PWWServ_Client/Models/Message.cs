﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWWServ_Client.Models
{
    public class Message
    {
        public DateTime WhenDT { get; set; }
        public string Owner { get; set; }
        public string Text { get; set; }
        public string StackTrace { get; set; }
    }
}