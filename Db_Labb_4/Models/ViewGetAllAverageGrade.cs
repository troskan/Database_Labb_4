using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class ViewGetAllAverageGrade
    {
        public string CourseName { get; set; } = null!;
        public int? AverageGrade { get; set; }
        public int? LowestGrade { get; set; }
        public int? HighestGrade { get; set; }
    }
}
