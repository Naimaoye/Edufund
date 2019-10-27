using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrippleNT.DBConnect;
using TrippleNT.Utility;

namespace TrippleNT.Model
{

    public class donatePayload
    {
        public long userId { get; set; }
        public long companyId { get; set; }
        public decimal amount { get; set; }

        public string phoneNumber { get; set; }
    }
    public class donateResponse
    {
        public string status { get; set; }
        public string msg { get; set; }
    }
    public class Donate
    {

        public donateResponse donate(donatePayload donate, IConfiguration _config,IHostingEnvironment env)
        {
            try
            {
                using (var db = new TrippleNTDBContext()) {
                    var reco = db.Reconciliation.Where(o => o.CompanyId == donate.companyId && o.Status == "Pending").FirstOrDefault();
                    if(reco!=null) return new donateResponse { status = "failed", msg = "Cannot commit donations becuase you have pending reconcilation payment " };
                    var donation = new Donations();
                    donation.DateDonated = DateTime.Now;
                    donation.CompanyStaffId = donate.userId;
                    donation.ComapanyId = donate.companyId;
                    donation.Amount = donate.amount;
                    donation.Donor = donate.phoneNumber;
                    donation.Status = "NR";
                    db.Donations.Add(donation);
                    db.SaveChanges();
                    var donor = db.Donors.Find(donate.phoneNumber);
                    if (donor == null)
                    {
                        var d = new Donors();

                        d.PhoneNumber = donate.phoneNumber;
                        db.Donors.Add(d);
                        db.SaveChanges();
                       
                    }
                    var msg = "Hello, thank you for your donation, Edufund appreciates your commitment to changing the world with your 'SPARE CHANGE'. Your can view your donation badge here https://bit.ly/fRruwQE";
                    var a = SendSMS.Send(donate.phoneNumber, msg, _config);
                    return new donateResponse { status = "success", msg = "Donation was Successful. Thank You" };
                }
            }
            catch(Exception ex)
            {
                new Utility.LogWriter(ex.Message + " " + ex.InnerException, env);
               return  new donateResponse { status = "failed", msg = "Something went wrong try again Later" };
            }
        }

    }
}
