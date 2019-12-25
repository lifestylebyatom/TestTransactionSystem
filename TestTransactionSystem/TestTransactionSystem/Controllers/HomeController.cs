using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTransactionSystem.Constants;
using TestTransactionSystem.Managers;

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

            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
               
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string fileEx = Path.GetExtension(fileName);
                    System.IO.Stream fileContent = file.InputStream;

                    switch (fileEx.ToLower())
                    {
                        case Common.FileImportExtention.csv:
                            _CSVManager.GetImportList(fileContent);
                            break;
                        case Common.FileImportExtention.xml:
                            _XMLManager.GetImportList(fileContent);
                            break;
                        default:
                            message = "Unknown format";
                            break;
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