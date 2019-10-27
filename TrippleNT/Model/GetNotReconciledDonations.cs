using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrippleNT.DBConnect;
using TrippleNT.DBConnect;

namespace TrippleNT.Model
{

    public class getDonationsResponse
    {
        public long userId { get; set; }
        public string donor { get; set; }
    
        public string status { get; set; }
        public string dateDonated { get; set; }
        public decimal amount { get; set; }
    }
    public class GetNotReconciledDonations
    {
        public List<getDonationsResponse> getNotReconciledForCompany(long companyId)
        {
            try
            {
                using(var db= new TrippleNTDBContext())
                {
                    var donations = db.Donations.Where(o => o.ComapanyId == companyId && o.Status == "NR").Select(o => new getDonationsResponse { amount=o.Amount, dateDonated=o.DateDonated.ToString("dd-mmm-yyyy"), status="Not Reconciled",donor=o.Donor.Substring(0,2)+"*****"+ o.Donor.Substring(8), userId=o.CompanyStaffId}).ToList();
                    return donations;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
