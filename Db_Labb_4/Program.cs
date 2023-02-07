using Db_Labb_4.Models;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Db_Labb_4
{
    internal class Program
    {
        static private School_Labb_2Context DB { get; set; } = new School_Labb_2Context();


        static void Main(string[] args)
        {
            MainMenu();
        }
        static void MainMenu()
        {
            string[] menuOptions = { "\t1. Get all students", "\t2. Get all students in a class", "\t3. Add new staff", "\t4. Display", "\t5. X", "\t6. X", "\t7. Exit" };
            int selectedOption = 0;

            while (true)
            {
                // Visa menyn
                Console.Clear();
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }


                // Hantera input från användaren
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && selectedOption > 0)
                {
                    selectedOption--;
                }
                else if (key == ConsoleKey.DownArrow && selectedOption < menuOptions.Length - 1)
                {
                    selectedOption++;
                }
                else if (key == ConsoleKey.Enter)
                {
                    switch (selectedOption)
                    {
                        case 0:
                            GetAllStudentsMenu();
                            Console.ReadKey();
                            break;

                        case 1:
                            GetAllClassesMenu();
                            Console.ReadKey();
                            break;

                        case 2:
                            AddStaffMenu();
                            Console.ReadKey();
                            break;

                        case 3:
                            DisplayStaffAtDepartments();
                            Console.ReadKey();
                            break;

                        case 4:
                            Console.ReadKey();
                            break;

                        case 5:
                            Console.ReadKey();
                            break;

                        case -1:
                            Console.WriteLine("TEST 3");
                            Console.ReadKey();
                            break;
                        default:
                            Environment.Exit(0);
                            break;
                    }

                }
            }
        }
        static void GetAllStudentsMenu()
        {
            string[] menuOptions = { "\t1. Sort by first name.", "\t2. Sort by last name.","\t3. Sort by first name + descending order."
                    ,"\t4. Sort by last name + descending order.","\t5. Exit." };
            int selectedOption = 0;

            while (true)
            {
                // Visa menyn
                Console.Clear();
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }

                // Hantera input från användaren
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && selectedOption > 0)
                {
                    selectedOption--;
                }
                else if (key == ConsoleKey.DownArrow && selectedOption < menuOptions.Length - 1)
                {
                    selectedOption++;
                }
                else if (key == ConsoleKey.Enter)
                {
                    switch (selectedOption)
                    {
                        case 0:
                            DisplayStudents(SortByFname());
                            PressKeyContinue();
                            break;

                        case 1:
                            DisplayStudents(SortByLname());
                            PressKeyContinue();
                            break;

                        case 2:
                            DisplayStudents(SortByFnameDesc());
                            PressKeyContinue();
                            break;

                        case 3:
                            DisplayStudents(SortByLnameDesc());
                            PressKeyContinue();
                            break;

                        default:
                            MainMenu();
                            break;
                    }

                }
            }
        }
        static void AddStaffMenu()
        {
            string[] menuOptions = { $"\t1. Add new Staff employee.", $"\t2. Back to Main Menu." };

            int selectedOption = 0;

            while (true)
            {
                // Visa menyn
                Console.Clear();
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }

                // Hantera input från användaren
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && selectedOption > 0)
                {
                    selectedOption--;
                }
                else if (key == ConsoleKey.DownArrow && selectedOption < menuOptions.Length - 1)
                {
                    selectedOption++;
                }
                else if (key == ConsoleKey.Enter)
                {
                    switch (selectedOption)
                    {
                        case 0:
                            RequestNewStaff();
                            PressKeyContinue();
                            MainMenu();
                            break;

                        default:
                            MainMenu();
                            break;
                    }

                }
            }
        }

        static void AddStaff(string fName, string lName, int ssn)
        {
            Staff s = new Staff();
            s.Fname = fName;
            s.Lname = lName;
            s.Ssn = ssn;

            DB.Staff.Add(s);
            DB.SaveChanges();
        }
        static void DisplayStudents(List<Student> list)
        {
            //Loop through results of collecion
            foreach (Student item in list)
            {
                Console.Write($"Student ID: {item.StudentId} "); TextRed("|");
                Console.Write($" Name: {item.Fname} {item.Lname} "); TextRed("|");
                Console.Write($" SSN: {FormatSSN(item.Ssn)}");
                Console.WriteLine();
            }
        }
        static void DisplayStaffAtDepartments()
        {
            var staff = DB.Staff.ToList();
            var dep = DB.Departments.ToList();
            var staffDep = DB.StaffDeps.ToList();

            var departmentResult = (from s in staff
                                    join staffd in staffDep on s.StaffId equals staffd.StaffId
                                    join d in dep on staffd.DepId equals d.DepId
                                    group s by d.DepName into grp
                                    select new
                                    {
                                        DepartmentName = grp.Key,
                                        StaffMembers = grp.ToList()
                                    }).ToList();

            foreach (var item in departmentResult)
            {
                Console.WriteLine("Department: " + item.DepartmentName);
                Console.WriteLine("Staff Members:");
                foreach (var staffMember in item.StaffMembers)
                {
                    Console.WriteLine("  " + staffMember.Fname);
                }
            }

            //var result = (from s in staff
            //              join sd in staffDep on s.Id equals sd.StaffId
            //              join d in dep on sd.DepId equals d.Id
            //              group s by d.DepName into grp
            //              select new
            //              {
            //                  DepartmentName = grp.Key,
            //                  StaffMembers = grp.ToList()
            //              }).ToList();

            //foreach (var item in result)
            //{
            //    Console.WriteLine("Department: " + item.DepartmentName);
            //    Console.WriteLine("Staff Members:");
            //    foreach (var staffMember in item.StaffMembers)
            //    {
            //        Console.WriteLine("  " + staffMember.Name);
            //    }
            //}


            //var query = from student in students
            //            join stuCourse in studentCourses on student.StudentId equals stuCourse.StudentId
            //            join course in courses on stuCourse.CourseId equals course.CourseId
            //            select new { student.Fname, student.StudentId, course.CourseName };


            //foreach (var item in query)
            //{
            //    Console.Write(item.ToString());
            //}


            //###################################

        }
        static string DisplayCourses(int input)
        {
            //Loop through results of collecion
            var courses = DB.Courses.ToList();
            int counter = 0;
            string course = "";
            foreach (Course item in courses)
            {
                counter++;
                if (counter == input)
                {
                    course = $"Course ID: {item.CourseId} | Course Name: {item.CourseName} | " +
                    $"Course Description: {item.CourseDesc}";
                }
            }
            return course;
        }
        static void DisplayStundetsInCourse(int courseID)
        {
            Console.Clear();

            var courses = DB.Courses.ToList();
            var students = DB.Students.ToList();
            var studentCourses = DB.StuCourses.ToList();

            int counter = 0;
            foreach (Course item in courses)
            {
                counter++;
                if (counter == courseID)
                {
                    Console.Write($"Course ID: {item.CourseId} "); TextRed("|");
                    Console.Write($" Course Name: {item.CourseName}"); TextRed("|");
                    Console.Write($" Course Description: {item.CourseDesc}");

                    Console.WriteLine("\n");
                    TextRed($"Students enrolled in {item.CourseName}");
                }
            }

            var StudentInClass = DB.StuCourses
               .Where(x => x.CourseId == courseID)
               .Select(x => x.Student);

            foreach (Student item in StudentInClass)
            {
                Console.WriteLine();
                Console.Write($"StudentID: {item.StudentId} "); TextRed("|");
                Console.Write($" Student Name: {item.Fname} {item.Lname} "); TextRed("|");
                Console.Write($" SSN: {FormatSSN(item.Ssn)}");
            }


            //#####################################


            //var query = from student in students
            //            join stuCourse in studentCourses on student.StudentId equals stuCourse.StudentId
            //            join course in courses on stuCourse.CourseId equals course.CourseId
            //            select new { student.Fname, student.StudentId, course.CourseName };


            //foreach (var item in query)
            //{
            //    Console.Write(item.ToString());
            //}


            //###################################





        }

        static void GetAllClassesMenu()
        {
            string[] menuOptions = { $"\t{DisplayCourses(1)}", $"\t{DisplayCourses(2)}", $"\t{DisplayCourses(3)}"
                    ,$"\t{DisplayCourses(4)}",$"\t{DisplayCourses(5)}", $"\t6. Back to Main Menu.", };

            int selectedOption = 0;

            while (true)
            {
                // Visa menyn
                Console.Clear();
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }

                // Hantera input från användaren
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && selectedOption > 0)
                {
                    selectedOption--;
                }
                else if (key == ConsoleKey.DownArrow && selectedOption < menuOptions.Length - 1)
                {
                    selectedOption++;
                }
                else if (key == ConsoleKey.Enter)
                {
                    switch (selectedOption)
                    {
                        case 0:
                            DisplayStundetsInCourse(1);
                            PressKeyContinue();
                            break;

                        case 1:
                            DisplayStundetsInCourse(2);

                            PressKeyContinue();
                            break;

                        case 2:
                            DisplayStundetsInCourse(3);

                            PressKeyContinue();
                            break;

                        case 3:
                            DisplayStundetsInCourse(4);

                            PressKeyContinue();
                            break;

                        case 4:
                            DisplayStundetsInCourse(5);

                            PressKeyContinue();
                            break;

                        default:
                            MainMenu();
                            break;
                    }

                }
            }
        }
        static void RequestNewStaff()
        {
            Console.Clear();
            Console.WriteLine("Enter first name of employee.");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter last name of employee.");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter employee Social Security Number, format= yyyy-mm-dd");
            int.TryParse(Console.ReadLine(), out int ssn);

            Console.Clear();
            Console.WriteLine("Do you swear on your life that these credentials are correct?");

            YesOrNoMenu(firstName, lastName, ssn);


        }
        static void YesOrNoMenu(string fname, string lname, int ssn)
        {
            string[] menuOptions = { $"\t1. Yes I swear on my life it's correct.", $"\tNO, THESE ARE NOT CORRECT, PLEASE UNDO!" };

            int selectedOption = 0;

            while (true)
            {
                // Visa menyn
                Console.Clear();
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }

                // Hantera input från användaren
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && selectedOption > 0)
                {
                    selectedOption--;
                }
                else if (key == ConsoleKey.DownArrow && selectedOption < menuOptions.Length - 1)
                {
                    selectedOption++;
                }
                else if (key == ConsoleKey.Enter)
                {
                    switch (selectedOption)
                    {
                        case 0:
                            AddStaff(fname, lname, ssn);
                            PressKeyContinue();
                            MainMenu();
                            break;

                        default:
                            MainMenu();
                            break;
                    }

                }
            }
        }
        static void PressKeyContinue()
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Press any key to continue..");
            Console.ResetColor();
            Console.ReadKey();
        }
        static void TextRed(string yourText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(yourText);
            Console.ResetColor();
        }
        static string FormatSSN(int ssnInput)
        {
            string ssn = ssnInput.ToString();
            string formattedSsn = ssn.Substring(0, 4) + "-" + ssn.Substring(4, 2) + "-" + ssn.Substring(6);

            return formattedSsn;
        }
        static List<Student> SortByFname()
        {
            var sortBy = DB.Students.OrderBy(s => s.Fname).ToList();
            return sortBy;
        }
        static List<Student> SortByLname()
        {
            var sortBy = DB.Students.OrderBy(s => s.Lname).ToList();
            return sortBy;
        }
        static List<Student> SortByFnameDesc()
        {
            var sortBy = DB.Students.OrderByDescending(s => s.Fname).ToList();
            return sortBy;
        }
        static List<Student> SortByLnameDesc()
        {
            var sortBy = DB.Students.OrderByDescending(s => s.Lname).ToList();
            return sortBy;
        }


    }
}
