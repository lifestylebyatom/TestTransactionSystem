using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTransactionSystem.Models.DataModel;
using TestTransactionSystem.Models.ViewModel;

namespace TestTransactionSystem.Managers
{
    public class TransactionManager
    {
        TestTransactionSystemEntities db = new TestTransactionSystemEntities();
        public bool  insert(List<TransactionViewModel> model)
        {
            LoggerManager _logger = new LoggerManager();
            try
            {
                foreach (var i in model)
                {
                    var item = new Transaction();

                    item.TransactionId = i.TransactionId;
                    item.TransactionDate = i.TransactionDate;
                    item.Amount = i.Amount;
                    item.CurrencyCode = i.CurrencyCode;
                    item.Status = i.Status;

                    db.Transactions.Add(item);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }

            return true;
        }
    }
}