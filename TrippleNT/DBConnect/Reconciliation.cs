using System;
using System.Collections.Generic;

namespace TrippleNT.DBConnect
{
    public partial class Reconciliation
    {
        public Reconciliation()
        {
            Payments = new HashSet<Payments>();
        }

        public long ReconcileId { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public long CompanyId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
    }
}
