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
                db = new TestTransactionSystemEntities();
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
                toReturn = true;
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

        public List<TransactionExportViewModel> GetByCurrency(string id)
        {
            LoggerManager _logger = new LoggerManager();
            var  toReturn = new List<TransactionExportViewModel>();

            try
            {
                db = new TestTransactionSystemEntities();
                var selectList = db.Transactions.Where(x => x.CurrencyCode == id).ToList();

                foreach (var s in selectList)
                {
                    var i = new TransactionExportViewModel();
                    i.id = s.TransactionId;
                    i.payment = $"{s.Amount} {s.CurrencyCode}";
                    i.Status = s.Status;

                    toReturn.Add(i);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            finally
            {
                db.Dispose();
            }

            return toReturn;
        }

        public List<TransactionExportViewModel> GetByDateRange(DateTime? DateFrom ,DateTime? DateTo)
        {
            LoggerManager _logger = new LoggerManager();
            var toReturn = new List<TransactionExportViewModel>();

            try
            {
                db = new TestTransactionSystemEntities();
                var selectList = db.Transactions.ToList();
                if (DateFrom.HasValue)
                {
                    selectList.Where(x => x.TransactionDate >= DateFrom.Value).ToList();
                }

                if (DateTo.HasValue)
                {
                    selectList.Where(x => x.TransactionDate <= DateFrom.Value).ToList();
                }

                foreach (var s in selectList)
                {
                    var i = new TransactionExportViewModel();
                    i.id = s.TransactionId;
                    i.payment = $"{s.Amount} {s.CurrencyCode}";
                    i.Status = s.Status;

                    toReturn.Add(i);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            finally
            {
                db.Dispose();
            }

            return toReturn;
        }

        public List<TransactionExportViewModel> GetByStatus(string id)
        {
            LoggerManager _logger = new LoggerManager();
            var toReturn = new List<TransactionExportViewModel>();

            try
            {
                db = new TestTransactionSystemEntities();
                var selectList = db.Transactions.Where(x => x.Status == id).ToList();

                foreach (var s in selectList)
                {
                    var i = new TransactionExportViewModel();
                    i.id = s.TransactionId;
                    i.payment = $"{s.Amount} {s.CurrencyCode}";
                    i.Status = s.Status;

                    toReturn.Add(i);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            finally
            {
                db.Dispose();
            }

            return toReturn;
        }
    }
}