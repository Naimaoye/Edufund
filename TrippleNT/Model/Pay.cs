using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrippleNT.DBConnect;

namespace TrippleNT.Model
{
    public class PaymentPayload
    {
      
        public long CompanyId { get; set; }

        public decimal amount  { get; set; }
        public string Reference { get; set; }
        public long UserId { get; set; }
        public long ReconcileId { get; set; }

    }
    public class PaymentResponse
    {
        public string status { get; set; }
        public string msg { get; set; }
    }
    public class Pay
    {

        public PaymentResponse NewRecord(PaymentPayload rcd)
        {

            try
            {
                using (var db = new TrippleNTDBContext())
                {

                    var reco = db.Reconciliation.Find(rcd.ReconcileId);
                    if(reco==null) return new PaymentResponse { status = "failed", msg = "Reconciliation ID not Found" };
                    var p = new Payments();
                    p.ReconcileId = rcd.ReconcileId;
                    p.Reference = rcd.Reference;
                    p.DateOfPayment = DateTime.Now;
                    p.UserId = rcd.UserId;
                    p.CompanyId = rcd.CompanyId;
                    p.Amount = rcd.amount;
                    db.Payments.Add(p);
                    reco.Status = "Paid";
                    db.Reconciliation.Attach(reco);
                    db.Entry(reco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    var lstunreco = db.Donations.Where(o => o.ComapanyId == rcd.CompanyId && o.DateDonated.Date <= reco.EndPeriod.Date && o.Status == "NR").ToList();
                    lstunreco.ForEach(o => o.Status = "R");
                    db.SaveChanges();
                    return new PaymentResponse { status="success",msg="Payment Successful" };
                }
            }
            catch (Exception ex)
            {
                return new PaymentResponse { status = "failed", msg= "Something went wrong try again later" };
            }
        }
    }
}
