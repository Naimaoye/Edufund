using System;
using System.Collections.Generic;

namespace TrippleNT.DBConnect
{
    public partial class Donations
    {
        public long DonationId { get; set; }
        public long ComapanyId { get; set; }
        public long CompanyStaffId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateDonated { get; set; }
        public string Donor { get; set; }
        public string Status { get; set; }

        public virtual Company Comapany { get; set; }
        public virtual CompanyStaff CompanyStaff { get; set; }
    }
}
