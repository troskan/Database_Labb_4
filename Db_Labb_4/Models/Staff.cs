using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class Staff
    {
        public Staff()
        {
            StaffDeps = new HashSet<StaffDep>();
            StaffRoles = new HashSet<StaffRole>();
        }

        public int StaffId { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public int Ssn { get; set; }
        public DateTime? DateHired { get; set; }
        public decimal? Salary { get; set; }

        public virtual ICollection<StaffDep> StaffDeps { get; set; }
        public virtual ICollection<StaffRole> StaffRoles { get; set; }
    }
}
