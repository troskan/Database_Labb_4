using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class ViewSalaryPayoutsDepartment
    {
        public string Department { get; set; } = null!;
        public decimal? TotalPayoutMonthly { get; set; }
        public int? Employees { get; set; }
    }
}
