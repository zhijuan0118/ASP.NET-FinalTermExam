using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPFinalExam.Models
{
    public class CustomerSearchArg
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
    }
}