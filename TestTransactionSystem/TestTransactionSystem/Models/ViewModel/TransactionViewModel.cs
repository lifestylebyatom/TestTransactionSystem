using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTransactionSystem.Models.ViewModel
{
    public class TransactionViewModel
    {
        public string TransactionId { get; set; }
        public Decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }

    public sealed class TransactionViewModelMap : ClassMap<TransactionViewModel>
    {
        public TransactionViewModelMap()
        {
            Map(m => m.TransactionId).Index(0);
            Map(m => m.Amount).Index(1);
            Map(m => m.CurrencyCode).Index(2);
            Map(m => m.TransactionDate).Index(3);
            Map(m => m.Status).Index(4);
        }
    }

}
