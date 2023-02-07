using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class ViewAverageSalaryDeparment
    {
        public string Department { get; set; } = null!;
        public decimal? AverageSalaryInDepartment { get; set; }
        public int? Employees { get; set; }
    }
}
