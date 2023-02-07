using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class StaffRole
    {
        public int StaffId { get; set; }
        public int StaffRoleId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Staff Staff { get; set; } = null!;
    }
}
