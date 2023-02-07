using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Db_Labb_4.Models
{
    public partial class School_Labb_2Context : DbContext
    {
        public School_Labb_2Context()
        {
        }

        public School_Labb_2Context(DbContextOptions<School_Labb_2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StaffDep> StaffDeps { get; set; } = null!;
        public virtual DbSet<StaffRole> StaffRoles { get; set; } = null!;
        public virtual DbSet<StuCourse> StuCourses { get; set; } = null!;
        public virtual DbSet<StuGrade> StuGrades { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<ViewAddStaff> ViewAddStaffs { get; set; } = null!;
        public virtual DbSet<ViewAverageSalaryDeparment> ViewAverageSalaryDeparments { get; set; } = null!;
        public virtual DbSet<ViewGetAllAverageGrade> ViewGetAllAverageGrades { get; set; } = null!;
        public virtual DbSet<ViewGetAllGrade> ViewGetAllGrades { get; set; } = null!;
        public virtual DbSet<ViewGetAllGradesFromThisMonth> ViewGetAllGradesFromThisMonths { get; set; } = null!;
        public virtual DbSet<ViewGetAllStaff> ViewGetAllStaffs { get; set; } = null!;
        public virtual DbSet<ViewSalaryPayoutsDepartment> ViewSalaryPayoutsDepartments { get; set; } = null!;
        public virtual DbSet<Staff> Staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-10QDP98;Database=School_Labb_2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("CourseID");

                entity.Property(e => e.CourseName).HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Courses_Roles");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepId);

                entity.Property(e => e.DepId)
                    .ValueGeneratedNever()
                    .HasColumnName("DepID");

                entity.Property(e => e.DepName).HasMaxLength(50);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.GradeId)
                    .ValueGeneratedNever()
                    .HasColumnName("GradeID");

                entity.Property(e => e.GradeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<StaffDep>(entity =>
            {
                entity.ToTable("StaffDep");

                entity.Property(e => e.StaffDepId).HasColumnName("StaffDepID");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.StaffDeps)
                    .HasForeignKey(d => d.DepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffDep_Departments");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffDeps)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffDep_Staff");
            });

            modelBuilder.Entity<StaffRole>(entity =>
            {
                entity.Property(e => e.StaffRoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("StaffRoleID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.StaffRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffRoles_Roles");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffRoles)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffRoles_Staff");
            });

            modelBuilder.Entity<StuCourse>(entity =>
            {
                entity.Property(e => e.StuCourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("StuCourseID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StuCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StuCourses_Courses");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StuCourses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StuCourses_Students");
            });

            modelBuilder.Entity<StuGrade>(entity =>
            {
                entity.Property(e => e.StuGradeId).HasColumnName("StuGradeID");

                entity.Property(e => e.GradeDate).HasColumnType("date");

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.StuGrades)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_StuGrades_Grades");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.StuGrades)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_StuGrades_Roles");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StuGrades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StuGrades_Students");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .HasColumnName("LName");

                entity.Property(e => e.Ssn).HasColumnName("SSN");
            });

            modelBuilder.Entity<ViewAddStaff>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_AddStaff");

                entity.Property(e => e.DateHired).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(101);

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.StaffId).HasColumnName("StaffID");
            });

            modelBuilder.Entity<ViewAverageSalaryDeparment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_AverageSalaryDeparments");

                entity.Property(e => e.AverageSalaryInDepartment)
                    .HasColumnType("money")
                    .HasColumnName("Average Salary In Department");

                entity.Property(e => e.Department).HasMaxLength(50);
            });

            modelBuilder.Entity<ViewGetAllAverageGrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_GetAllAverageGrades");

                entity.Property(e => e.CourseName).HasMaxLength(50);
            });

            modelBuilder.Entity<ViewGetAllGrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_GetAllGrades");

                entity.Property(e => e.CourseName).HasMaxLength(50);

                entity.Property(e => e.GradeDate).HasColumnType("date");

                entity.Property(e => e.GradeName).HasMaxLength(50);

                entity.Property(e => e.SetBy).HasMaxLength(50);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.StudentName).HasMaxLength(101);
            });

            modelBuilder.Entity<ViewGetAllGradesFromThisMonth>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_GetAllGradesFromThisMonth");

                entity.Property(e => e.CourseName).HasMaxLength(50);

                entity.Property(e => e.GradeDate).HasColumnType("date");

                entity.Property(e => e.GradeName).HasMaxLength(50);

                entity.Property(e => e.SetBy).HasMaxLength(50);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.StudentName).HasMaxLength(101);
            });

            modelBuilder.Entity<ViewGetAllStaff>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_GetAllStaff");

                entity.Property(e => e.DateHired).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(101);

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.StaffId).HasColumnName("StaffID");
            });

            modelBuilder.Entity<ViewSalaryPayoutsDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_SalaryPayoutsDepartment");

                entity.Property(e => e.Department).HasMaxLength(50);

                entity.Property(e => e.TotalPayoutMonthly)
                    .HasColumnType("money")
                    .HasColumnName("Total Payout Monthly");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.DateHired).HasColumnType("date");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .HasColumnName("LName");

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.Property(e => e.Ssn).HasColumnName("SSN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
