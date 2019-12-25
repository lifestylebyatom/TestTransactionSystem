using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTransactionSystem.Constants
{
    public class Common
    {
        public struct FileImportExtention
        {
            public const string csv = ".csv";
            public const string xml = ".xml";
        }

        public struct CSVStatus
        {
            public const string Approved = "Approved";
            public const string Failed = "Failed";
            public const string Finished = "Finished";
        }

        public struct XMLStatus
        {
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
            public const string Done = "Done";
        }

        public struct Status
        {
            public const string Approved = "A";
            public const string Rejected = "R";
            public const string Done = "D";
        }
    }
}