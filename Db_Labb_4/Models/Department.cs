using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class Department
    {
        public Department()
        {
            StaffDeps = new HashSet<StaffDep>();
        }

        public int DepId { get; set; }
        public string DepName { get; set; } = null!;

        public virtual ICollection<StaffDep> StaffDeps { get; set; }
    }
}
