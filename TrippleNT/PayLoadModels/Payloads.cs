using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrippleNT.ResponseModels
{
    public class Payloads
    {

        public class AddCompanyPayload
        {
            public string companyName { get; set; }
            public string email { get; set; }
            public string location { get; set; }
            public string state { get; set; }
            public string phoneNumber { get; set; }
            public int? noOfStaff { get; set; }

        }
        public class AddCompanyIndPayload
        {

            public string companyName { get; set; }
            public string email { get; set; }
            public string location { get; set; }
            public string state { get; set; }
            public string phoneNumber { get; set; }
            public string rcNo { get; set; }
            public string  password { get; set; }
        }
    }
}
