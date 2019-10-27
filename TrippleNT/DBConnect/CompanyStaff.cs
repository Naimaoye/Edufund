using System;
using System.Collections.Generic;

namespace TrippleNT.DBConnect
{
    public partial class CompanyStaff
    {
        public CompanyStaff()
        {
            Donations = new HashSet<Donations>();
            Payments = new HashSet<Payments>();
        }

        public long UserId { get; set; }
        public long CompanyId { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public bool UseStatus { get; set; }
        public string UserType { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Donations> Donations { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
    }
}
