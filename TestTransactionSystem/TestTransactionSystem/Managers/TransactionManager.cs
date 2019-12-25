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
        public bool InsertTransaction(List<TransactionViewModel> model)
        {
            LoggerManager _logger = new LoggerManager();
            var dtNow = DateTime.Now;
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            bool toReturn = false;

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

                    item.CreateBy = userName;
                    item.CreateDate = dtNow;
                    item.UpdateBy = userName;
                    item.UpdateDate = dtNow;

                    db.Transactions.Add(item);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            finally {
                db.Dispose();
            }

            return toReturn;
        }
    }
}