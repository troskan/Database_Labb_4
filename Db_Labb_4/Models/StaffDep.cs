using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class StaffDep
    {
        public int StaffDepId { get; set; }
        public int StaffId { get; set; }
        public int DepId { get; set; }

        public virtual Department Dep { get; set; } = null!;
        public virtual Staff Staff { get; set; } = null!;
    }
}
