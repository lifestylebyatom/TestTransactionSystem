using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTransactionSystem.Constants;
using TestTransactionSystem.Managers;
using TestTransactionSystem.Models.ViewModel;

namespace TestTransactionSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            bool isVaild = false;
            string message = "";

            LoggerManager _logger = new LoggerManager();
            CSVManager _CSVManager = new CSVManager();
            XMLManager _XMLManager = new XMLManager();
            TransactionManager _TransactionManager = new TransactionManager();
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
               
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string fileEx = Path.GetExtension(fileName);
                    System.IO.Stream fileContent = file.InputStream;
                    var InsertList = new List<TransactionViewModel>();

                    switch (fileEx.ToLower())
                    {
                        case Common.FileImportExtention.csv:
                            InsertList =  _CSVManager.GetImportList(fileContent);
                            break;
                        case Common.FileImportExtention.xml:
                            InsertList = _XMLManager.GetImportList(fileContent);
                            break;
                        default:
                            message = "Unknown format";
                            break;
                    }

                    if (InsertList.Count>0)
                    {
                        isVaild = _TransactionManager.InsertTransaction(InsertList);
                    }

                    if (!isVaild)
                    {
                        message = "File import not success.";
                        break;
                    }
                    else {
                        message = "File import success.";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                message = $"Error: {ex.Message}";
            }

            return Json(message);
        }
    }
}