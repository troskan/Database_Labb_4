using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class Role
    {
        public Role()
        {
            Courses = new HashSet<Course>();
            StaffRoles = new HashSet<StaffRole>();
            StuGrades = new HashSet<StuGrade>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDesc { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<StaffRole> StaffRoles { get; set; }
        public virtual ICollection<StuGrade> StuGrades { get; set; }
    }
}
