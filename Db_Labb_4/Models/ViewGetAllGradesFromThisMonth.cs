using System;
using System.Collections.Generic;

namespace Db_Labb_4.Models
{
    public partial class ViewGetAllGradesFromThisMonth
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string GradeName { get; set; } = null!;
        public DateTime GradeDate { get; set; }
        public string? GradeJustification { get; set; }
        public string SetBy { get; set; } = null!;
    }
}
