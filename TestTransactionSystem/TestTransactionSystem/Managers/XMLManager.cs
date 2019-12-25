using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using TestTransactionSystem.Constants;
using TestTransactionSystem.Models.ViewModel;

namespace TestTransactionSystem.Managers
{
    public class XMLManager
    {
        public List<TransactionViewModel> GetImportList(Stream fileStream)
        {
            var toReturn = new List<TransactionViewModel>();

            LoggerManager _logger = new LoggerManager();
            try
            {
                XDocument xmlReadFile = XDocument.Load(fileStream);
                var result = xmlReadFile.Descendants().ToList();

                var transactionItems = xmlReadFile.Descendants("Transactions")
                .Elements("Transaction")
                .ToList();

                if (transactionItems.Count > 0)
                {
                    foreach (var t in transactionItems)
                    {
                        var i = new TransactionViewModel();

                        var transaction_id = t.Attributes("id").First();
                        i.Transaction_id = transaction_id.Value;

                        var transactionDate = t.Descendants("TransactionDate").First();
                        i.Transaction_Date = DateTime.ParseExact(transactionDate.Value, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                        var Amount = t.Descendants("PaymentDetails").Elements("Amount").First();
                        i.Amount = Convert.ToDecimal(Amount.Value);

                        var CurrencyCode = t.Descendants("PaymentDetails").Elements("CurrencyCode").First();
                        i.CurrencyCode = CurrencyCode.Value;

                        var status = t.Descendants("Status").First();
                        i.Status = MappingStatus(status.Value);

                        toReturn.Add(i);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }

            return toReturn;
        }

        private string MappingStatus(string status)
        {
            var toReturn = string.Empty;
            LoggerManager _logger = new LoggerManager();
            try
            {
                switch (status)
                {
                    case Common.XMLStatus.Approved:
                        toReturn = Common.Status.Approved;
                        break;
                    case Common.XMLStatus.Rejected:
                        toReturn = Common.Status.Rejected;
                        break;
                    case Common.XMLStatus.Done:
                        toReturn = Common.Status.Done;
                        break;
                    default:
                        throw new Exception("XML mapping status not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw ex;
            }

            return toReturn;
        }
    }
}