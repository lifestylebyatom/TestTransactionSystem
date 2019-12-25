using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTransactionSystem.Models.ImportModel
{
    public class CSVImportModel
    {
        public string Transaction_id { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Transaction_Date { get; set; }
        public string Status { get; set; }
    }

    public sealed class CSVImportModelMap : ClassMap<CSVImportModel>
    {
        public CSVImportModelMap()
        {
            Map(m => m.Transaction_id).Index(0);
            Map(m => m.Amount).Index(1);
            Map(m => m.CurrencyCode).Index(2);
            Map(m => m.Transaction_Date).Index(3);
            Map(m => m.Status).Index(4);
        }
    }
}