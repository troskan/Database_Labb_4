using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class ViewGetAllStaff
    {
        public int StaffId { get; set; }
        public string Name { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public DateTime? DateHired { get; set; }
        public int? YearsWorked { get; set; }
    }
}
