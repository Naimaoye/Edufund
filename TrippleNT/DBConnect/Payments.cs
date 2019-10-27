using System;
using System.Collections.Generic;

namespace TrippleNT.DBConnect
{
    public partial class Payments
    {
        public long PaymentId { get; set; }
        public long CompanyId { get; set; }
        public DateTime DateOfPayment { get; set; }
        public string Reference { get; set; }
        public long UserId { get; set; }
        public long ReconcileId { get; set; }
        public decimal? Amount { get; set; }

        public virtual Company Company { get; set; }
        public virtual Reconciliation Reconcile { get; set; }
        public virtual CompanyStaff User { get; set; }
    }
}
