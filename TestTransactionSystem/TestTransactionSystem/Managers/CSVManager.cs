using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using TestTransactionSystem.Models.ViewModel;
using TestTransactionSystem.Models.ImportModel;
using CsvHelper;

namespace TestTransactionSystem.Managers
{
    public class CSVManager
    {

        public List<TransactionViewModel> GetImportList(Stream fileStream)
        {
            var toReturn = new List<TransactionViewModel>();
            var CSVReaderList = new List<CSVImportModel>();

            LoggerManager _logger = new LoggerManager();
            try
            {
                using (var reader = new StreamReader(fileStream))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Configuration.RegisterClassMap<CSVImportModelMap>();
                    csv.Configuration.Quote = '"';
                    csv.Configuration.BadDataFound = null;

                    while (csv.Read())
                    {
                        CSVReaderList.Add(csv.GetRecord<CSVImportModel>());
                    }
                }

                if (CSVReaderList.Count > 0)
                {
                    foreach (var item in CSVReaderList)
                    {
                        var i = new TransactionViewModel();

                        if (!string.IsNullOrEmpty(item.Transaction_id))
                        {
                            i.Transaction_id = ReplaceString(item.Transaction_id);
                        }
                        else
                        {
                            throw new Exception("Transaction_id is null or empty.");
                        }

                        if (!string.IsNullOrEmpty(item.Amount))
                        {
                            i.Amount = Decimal.Parse(ReplaceDecimal(item.Amount));
                        }
                        else
                        {
                            throw new Exception($"{i.Transaction_id} - Amount is null or empty.");
                        }

                        if (!string.IsNullOrEmpty(item.CurrencyCode))
                        {
                            i.CurrencyCode = ReplaceString(item.CurrencyCode).ToUpper();
                        }
                        else
                        {
                            throw new Exception($"{i.Transaction_id} - CurrencyCode is null or empty.");
                        }

                        if (!string.IsNullOrEmpty(item.Transaction_Date))
                        {
                            i.Transaction_Date = DateTime.ParseExact(ReplaceString(item.Transaction_Date), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            throw new Exception($"{i.Transaction_id} - Transaction_Date is null or empty.");
                        }

                        if (!string.IsNullOrEmpty(item.Status))
                        {
                            i.Status = MappingStatus(ReplaceString(item.Status));
                        }
                        else
                        {
                            throw new Exception($"{i.Transaction_id} - Status is null or empty.");
                        }

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

        public string ReplaceString(string message)
        {
            var toReturn = string.Empty;
            try
            {
                toReturn = message.Trim().Replace("\"", "");
            }
            catch (Exception)
            {

                throw;
            }

            return toReturn;
        }
        public string ReplaceDecimal(string message)
        {
            var toReturn = string.Empty;
            try
            {
                toReturn = ReplaceString(message).Replace(",", "");
            }
            catch (Exception)
            {

                throw;
            }

            return toReturn;
        }


        public string MappingStatus(string status)
        {
            var toReturn = string.Empty;
            try
            {
                switch (status)
                {
                    case Common.CSVStatus.Approved:
                        toReturn = Common.Status.Approved;
                        break;
                    case Common.CSVStatus.Failed:
                        toReturn = Common.Status.Rejected;
                        break;
                    case Common.CSVStatus.Finished:
                        toReturn = Common.Status.Done;
                        break;
                }

                if (string.IsNullOrEmpty(toReturn))
                {
                    throw new Exception("CSV mapping status not found");
                }

            }
            catch (Exception ex)
            {
                //TODO Write Log
                throw;
            }

            return toReturn;
        }

    }
}