using System.Collections.Concurrent;

namespace StudentResult
{
    internal class Program
    {
        static List<Student> students = new List<Student>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Student Info Management Center.!");
                Console.WriteLine("==== STUDENT RESULT CONSOLE APP ====");
                Console.WriteLine("1. Add a Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Update Student Info");
                Console.WriteLine("4. Delete a Student's Info");
                Console.WriteLine("5. Search Student by Id");
                Console.WriteLine("6. Search Student by Name");
                Console.WriteLine("7. Mark for Highest, Lowest and Average Mark");
                Console.WriteLine("8. Display Passed Students");
                Console.WriteLine("9. Display Failed Students");
                Console.WriteLine("10. Display Students by Grade");
                Console.WriteLine("11. Filter Students Above Average");
                Console.WriteLine("12. Sort Students by Mark");
                Console.WriteLine("13. Display Top Performer");
                Console.WriteLine("0. Exit");
                Console.Write("\nEnter Choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                Console.Clear();
                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        DisplayStudents();
                        break;
                    case 3:
                        UpdateStudent();
                        break;
                    case 4:
                        DeleteStudent();
                        break;
                    case 5:
                        SearchById();
                        break;
                    case 6:
                        SeachByName();
                        break;
                    case 7:
                        ShowMarks();
                        break;
                    case 8:
                        PassedStudents();
                        break;
                    case 9:
                        FailedStudents();
                        break;
                    case 10:
                        DisplayByGrade();
                        break;
                    case 11:
                        AboveAverage();
                        break;
                    case 12:
                        SortByMark();
                        break;
                    case 13:
                        TopPerformer();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;

                }
                Console.WriteLine("\n\nPress any Key to End the Program...");
                Console.ReadKey();
            }

        }

        static void AddStudent()
        {
            Student student = new Student();
            Console.Write("Enter ID: ");
            student.Id = Convert.ToInt32(Console.ReadLine());

            if (students.Any(x => x.Id == student.Id))
            {
                Console.WriteLine("ID Already Exists....");
                return;
            }
            Console.Write("Enter Name: ");
            student.Name = Console.ReadLine()!;

            Console.Write("Enter Marks: ");
            student.Mark = Convert.ToInt32(Console.ReadLine());
            if (student.Mark < 50)
            {
                student.IsPassed = false;
            }
            else
            {
                student.IsPassed = true;

            }
            student.Grade = CalculateGrade(student.Mark);
            students.Add(student);
            Console.WriteLine("Student Added Successfully!");
        }
        static void DisplayStudents()
        {
            if (!students.Any())
            {
                Console.WriteLine("No Student Found.");
                return;
            }

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("ID\tName\t\tMark\tGrade");
            Console.WriteLine("------------------------------------------------------");

            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id}\t{s.Name}\t\t{s.Mark}\t{s.Grade}");
            }

        }
        static void UpdateStudent()
        {
            Console.Write("Enter Student ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var student = students.FirstOrDefault(x => x.Id == id);

            if (student == null)
            {
                Console.WriteLine("Student Not Found.");
                return;
            }

            Console.Write("New Name: ");
            student.Name = Console.ReadLine()!;

            Console.Write("New Mark: ");
            student.Mark = Convert.ToInt32(Console.ReadLine());
            student.Grade = CalculateGrade(student.Mark);

            Console.WriteLine("Updated Successfully.");
        }
        static void DeleteStudent()
        {
            Console.Write("Enter Student ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var student = students.FirstOrDefault(x => x.Id == id);

            if (student == null)
            {
                Console.WriteLine("Student Not Found.");
                return;
            }

            students.Remove(student);

            Console.WriteLine("Student Removed.");
        }
        static void SearchById()
        {
            Console.Write("Enter Student ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var s = students.FirstOrDefault(x => x.Id == id);

            if (s == null)
            {
                Console.WriteLine("Student Not Found.");
                return;
            }

            Console.WriteLine($"ID : {s.Id}");
            Console.WriteLine($"Name : {s.Name}");
            Console.WriteLine($"Mark : {s.Mark}");
            Console.WriteLine($"Grade : {s.Grade}");
        }
        static void SeachByName()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!.ToLower();

            var result = students.Where(x => x.Name.ToLower().Contains(name));

            if (!result.Any())
            {
                Console.WriteLine("No Student Found.");
                return;
            }

            foreach (var s in result)
            {
                Console.WriteLine($"{s.Id}  {s.Name}  {s.Mark}  {s.Grade}");
            }
        }
        static void ShowMarks()
        {
            if (!students.Any())
            {
                Console.WriteLine("No Student Data.");
                return;
            }

            Console.WriteLine($"Highest Mark : {students.Max(x => x.Mark)}");
            Console.WriteLine($"Lowest Mark : {students.Min(x => x.Mark)}");
            Console.WriteLine($"Average Mark : {students.Average(x => x.Mark):F2}");
        }
        static void PassedStudents()
        {
            var passed = students.Where(x => x.IsPassed);

            if (!passed.Any())
            {
                Console.WriteLine("No Passed Students.");
                return;
            }

            foreach (var s in passed)
            {
                Console.WriteLine($"{s.Id} {s.Name} {s.Mark} {s.Grade}");
            }
        }
        static void FailedStudents()
        {
            var failed = students.Where(x => !x.IsPassed);

            if (!failed.Any())
            {
                Console.WriteLine("No Failed Students.");
                return;
            }

            foreach (var s in failed)
            {
                Console.WriteLine($"{s.Id} {s.Name} {s.Mark} {s.Grade}");
            }
        }
        static void DisplayByGrade()
        {
            Console.Write("Enter Grade: ");
            string grade = Console.ReadLine()!.ToUpper();

            var result = students.Where(x => x.Grade == grade);

            if (!result.Any())
            {
                Console.WriteLine("No Student Found.");
                return;
            }

            foreach (var s in result)
            {
                Console.WriteLine($"{s.Id} {s.Name} {s.Mark}");
            }
        }
        static void AboveAverage()
        {
            if (!students.Any())
            {
                Console.WriteLine("No Student Found.");
                return;
            }

            double avg = students.Average(x => x.Mark);

            Console.WriteLine($"Average Mark = {avg:F2}\n");

            var result = students.Where(x => x.Mark > avg);

            foreach (var s in result)
            {
                Console.WriteLine($"{s.Id} {s.Name} {s.Mark}");
            }
        }
        static void SortByMark()
        {
            var sorted = students.OrderByDescending(x => x.Mark);

            foreach (var s in sorted)
            {
                Console.WriteLine($"{s.Id} {s.Name} {s.Mark}");
            }
        }
        static void TopPerformer()
        {
            if (!students.Any())
            {
                Console.WriteLine("No Student Found.");
                return;
            }

            var top = students.OrderByDescending(x => x.Mark).First();

            Console.WriteLine("Top Performer");
            Console.WriteLine("-------------------");
            Console.WriteLine($"ID : {top.Id}");
            Console.WriteLine($"Name : {top.Name}");
            Console.WriteLine($"Mark : {top.Mark}");
            Console.WriteLine($"Grade : {top.Grade}");
        }
        static string CalculateGrade(int mark)
        {
            if (mark >= 80) return "A+";
            else if (mark >= 70) return "A";
            else if (mark >= 60) return "A-";
            else if (mark >= 50) return "B+";
            else return "F";
        }



    }
}
