using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrippleNT.DBConnect;

namespace TrippleNT.Model
{
    public class GetDonationsByCompany
    {

        public List<getDonationsResponse> getDonationsForCompany(long companyId, DateTime startdate, DateTime enddate)
        {
            try
            {
                using (var db = new TrippleNTDBContext())
                {
                    var donations = db.Donations.Where(o => o.ComapanyId == companyId && o.Status == "NR" && o.DateDonated.Date>=startdate && o.DateDonated.Date<= enddate).Select(o => new getDonationsResponse { amount = o.Amount, dateDonated = o.DateDonated.ToString("dd-mmm-yyyy"), status = "Not Reconciled", donor = o.Donor.Substring(0, 2) + "*****" + o.Donor.Substring(8), userId = o.CompanyStaffId }).ToList();
                    return donations;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
