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
            CSVManager _CSVManager = new CSVManager();

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

                            break;
                        default:
                            message = "Unknown format";
                            break;
                    }

                    if (!isVaild)
                        break;

                    message = "File import success.";
                }
            }
            catch (Exception)
            {

                throw;
            }




            return Json(message);
        }
    }
}