//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PWWebService
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransferEntity
    {
        public long id { get; set; }
        public System.DateTime whendt { get; set; }
        public decimal summa { get; set; }
        public decimal amount { get; set; }
        public string client { get; set; }
        public string notes { get; set; }
        public Nullable<long> source { get; set; }
    }
}