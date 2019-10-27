using System;
using System.Collections.Generic;

namespace TrippleNT.DBConnect
{
    public partial class Company
    {
        public Company()
        {
            CompanyStaff = new HashSet<CompanyStaff>();
            Donations = new HashSet<Donations>();
            Payments = new HashSet<Payments>();
            Reconciliation = new HashSet<Reconciliation>();
        }

        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public DateTime RegDate { get; set; }
        public int Type { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Rcno { get; set; }

        public virtual ICollection<CompanyStaff> CompanyStaff { get; set; }
        public virtual ICollection<Donations> Donations { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
        public virtual ICollection<Reconciliation> Reconciliation { get; set; }
    }
}
