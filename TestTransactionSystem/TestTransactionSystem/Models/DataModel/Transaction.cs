//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestTransactionSystem.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string Status { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}
