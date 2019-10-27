using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrippleNT.DBConnect;

namespace TrippleNT.Model
{
   
    public class reconcileResponse
    {
        public int value { get; set; }
    }
    public class Reconcile
    {
       public object NewRecord( Reconciliation  rcd)
        {

            try
            {
                using (var db= new TrippleNTDBContext())
                {
                    db.Reconciliation.Add(rcd);
                    db.SaveChanges();
                    return new reconcileResponse { value = 1 };
                }
            }
            catch(Exception ex)
            {
                return new reconcileResponse { value = 0 };
            }
        }


    }
}
