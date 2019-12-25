using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestTransactionSystem.Managers
{
    public class LoggerManager
    {

        public static string strLogFilePath = string.Empty;
        private static StreamWriter sw = null;

        public string LogFilePath
        {
            set
            {
                strLogFilePath = value;
            }
            get
            {
                return strLogFilePath;
            }
        }

        private bool WriteErrorLog(string strPathName,
                                Exception objException)
        {
            bool bReturn = false;
            string strException = string.Empty;
            try
            {
                sw = new StreamWriter(strPathName, true);
                sw.WriteLine("Source        : " +
                        objException.Source.ToString().Trim());
                sw.WriteLine("Method        : " +
                        objException.TargetSite.Name.ToString());
                sw.WriteLine("Date        : " +
                        DateTime.Now.ToLongTimeString());
                sw.WriteLine("Time        : " +
                        DateTime.Now.ToShortDateString());
                sw.WriteLine("Error        : " +
                        objException.Message.ToString().Trim());
                sw.WriteLine("Stack Trace    : " +
                        objException.StackTrace.ToString().Trim());
                sw.WriteLine("------------------------------------------------------------------");
        
                sw.Flush();
                sw.Close();
                bReturn = true;
            }
            catch (Exception)
            {
                bReturn = false;
            }
            return bReturn;
        }

        public void LogError(Exception objException)
        {
            WriteErrorLog(LogFilePath, objException);
        }

    }
}