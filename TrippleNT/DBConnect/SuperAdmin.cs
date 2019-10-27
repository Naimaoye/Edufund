using System;
using System.Collections.Generic;

namespace TrippleNT.DBConnect
{
    public partial class SuperAdmin
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
    }
}
