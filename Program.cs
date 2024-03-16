using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    public double Tuition { get; set; }
}

public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}

public class StudentGPA
{
    public int StudentID { get; set; }
    public double GPA { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Student collection
        IList<Student> studentList = new List<Student>()
        {
            new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major = "Hospitality", Tuition = 3500.00 },
            new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major = "Hospitality", Tuition = 4500.00 },
            new Student() { StudentID = 3, StudentName = "Cookie Crumb", Age = 21, Major = "CIT", Tuition = 2500.00 },
            new Student() { StudentID = 4, StudentName = "Ima Script", Age = 48, Major = "CIT", Tuition = 5500.00 },
            new Student() { StudentID = 5, StudentName = "Cora Coder", Age = 35, Major = "CIT", Tuition = 1500.00 },
            new Student() { StudentID = 6, StudentName = "Ura Goodchild", Age = 40, Major = "Marketing", Tuition = 500.00 },
            new Student() { StudentID = 7, StudentName = "Take Mewith", Age = 29, Major = "Aerospace Engineering", Tuition = 5500.00 }
        };

        // Student GPA Collection
        IList<StudentGPA> studentGPAList = new List<StudentGPA>()
        {
            new StudentGPA() { StudentID = 1, GPA = 4.0 },
            new StudentGPA() { StudentID = 2, GPA = 3.5 },
            new StudentGPA() { StudentID = 3, GPA = 2.0 },
            new StudentGPA() { StudentID = 4, GPA = 1.5 },
            new StudentGPA() { StudentID = 5, GPA = 4.0 },
            new StudentGPA() { StudentID = 6, GPA = 2.5 },
            new StudentGPA() { StudentID = 7, GPA = 1.0 }
        };

        // Club collection
        IList<StudentClubs> studentClubList = new List<StudentClubs>()
        {
            new StudentClubs() { StudentID = 1, ClubName = "Photography" },
            new StudentClubs() { StudentID = 1, ClubName = "Game" },
            new StudentClubs() { StudentID = 2, ClubName = "Game" },
            new StudentClubs() { StudentID = 5, ClubName = "Photography" },
            new StudentClubs() { StudentID = 6, ClubName = "Game" },
            new StudentClubs() { StudentID = 7, ClubName = "Photography" },
            new StudentClubs() { StudentID = 3, ClubName = "PTK" }
        };

        // a) Group by GPA and display the student's IDs
        var queryA = studentGPAList.GroupBy(s => s.GPA);
        Console.WriteLine("Students Grouped by GPA:");
        foreach (var group in queryA)
        {
            Console.WriteLine($"GPA: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"StudentID: {student.StudentID}");
            }
        }

        // b) Sort by Club, then group by Club and display the student's IDs
        var queryB = studentClubList.OrderBy(s => s.ClubName).GroupBy(s => s.ClubName);
        Console.WriteLine("\nStudents Grouped by Club:");
        foreach (var group in queryB)
        {
            Console.WriteLine($"Club: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"StudentID: {student.StudentID}");
            }
        }

        // c) Count the number of students with a GPA between 2.5 and 4.0
        var queryC = studentGPAList.Count(s => s.GPA >= 2.5 && s.GPA <= 4.0);
        Console.WriteLine($"\nNumber of students with GPA between 2.5 and 4.0: {queryC}");

        // d) Average all student's tuition
        var queryD = studentList.Average(s => s.Tuition);
        Console.WriteLine($"\nAverage Tuition: {queryD:C}");

        // e) Find the student paying the most tuition and display their name, major and tuition
        var highestTuition = studentList.Max(s => s.Tuition);
        var queryE = studentList.Where(s => s.Tuition == highestTuition);
        Console.WriteLine("\nStudent(s) with the Highest Tuition:");
        foreach (var student in queryE)
        {
            Console.WriteLine($"Name: {student.StudentName}, Major: {student.Major}, Tuition: {student.Tuition:C}");
        }

        // f) Join the student list and student GPA list on student ID and display the student's name, major and gpa
        var queryF = studentList.Join(studentGPAList,
                                       student => student.StudentID,
                                       gpa => gpa.StudentID,
                                       (student, gpa) => new { student.StudentName, student.Major, gpa.GPA });
        Console.WriteLine("\nStudent Name, Major, and GPA:");
        foreach (var student in queryF)
        {
            Console.WriteLine($"Name: {student.StudentName}, Major: {student.Major}, GPA: {student.GPA}");
        }

        // g) Join the student list and student club list. Display the names of only those students who are in the Game club.
        var queryG = studentList.Join(studentClubList.Where(s => s.ClubName == "Game"),
                                       student => student.StudentID,
                                       club => club.StudentID,
                                       (student, club) => student.StudentName);
        Console.WriteLine("\nStudents in the Game Club:");
        foreach (var student in queryG)
        {
            Console.WriteLine($"Name: {student}");
        }
    }
}
