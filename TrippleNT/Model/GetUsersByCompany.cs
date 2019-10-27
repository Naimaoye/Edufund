using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrippleNT.DBConnect;

namespace TrippleNT.Model
{

    public class getUsersByCompanyResponse
    {
        public long userId { get; set; }
        public string status { get; set; }
        public string role { get; set; }
        public string  password { get; set; }
        public string regDate { get; set; }
        public bool useStatus { get;set;}
    }
    public class GetUsersByCompany
    {
        public List<getUsersByCompanyResponse> getUserByCompany(long companyId, IHostingEnvironment env )
        {
            try
            {
                using (var db = new TrippleNTDBContext())
                {
                    var users = db.CompanyStaff.Where(o => o.CompanyId == companyId).Select(o => new getUsersByCompanyResponse { userId=o.UserId,status= o.Status,useStatus=o.UseStatus, role=o.UserType, regDate=o.DateCreated.ToString("dd-mmm-yyyy"),password=o.Password }).ToList();
                    return users;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
