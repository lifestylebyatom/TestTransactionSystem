using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestTransactionSystem.Managers;
using TestTransactionSystem.Models.DataModel;
using TestTransactionSystem.Models.ViewModel;

namespace TestTransactionSystem.ApiControllers
{
    public class GetTransactionController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(TransactionExportViewModel))]
        public HttpResponseMessage ByCurrency(string id)
        {
            var toReturn = new List<TransactionExportViewModel>();
            LoggerManager _logger = new LoggerManager(); 
            TransactionManager _TransactionManager = new TransactionManager();
            TestTransactionSystemEntities dbContext = new TestTransactionSystemEntities();
            string jsonData = string.Empty;
            try
            {
                toReturn = _TransactionManager.GetByCurrency(id);

                jsonData = JsonConvert.SerializeObject(toReturn);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                return response;
                throw;
            }


        }

        [HttpGet]
        [ResponseType(typeof(TransactionExportViewModel))]
        public HttpResponseMessage ByStatus(string id)
        {
            var toReturn = new List<TransactionExportViewModel>();
            LoggerManager _logger = new LoggerManager();
            TransactionManager _TransactionManager = new TransactionManager();
            string jsonData = string.Empty;
            try
            {
                toReturn = _TransactionManager.GetByStatus(id);

                jsonData = JsonConvert.SerializeObject(toReturn);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                return response;
                throw;
            }


        }

        [HttpGet]
        [ResponseType(typeof(TransactionExportViewModel))]
        public HttpResponseMessage ByDateRange(DateTime? DateFrom , DateTime? DateTo)
        {
            var toReturn = new List<TransactionExportViewModel>();
            LoggerManager _logger = new LoggerManager();
            TransactionManager _TransactionManager = new TransactionManager();
            string jsonData = string.Empty;
            try
            {
                toReturn = _TransactionManager.GetByDateRange(DateFrom, DateTo);

                jsonData = JsonConvert.SerializeObject(toReturn);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                return response;
                throw;
            }


        }
    }
}
