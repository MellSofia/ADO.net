using Microsoft.EntityFrameworkCore;

namespace UniversityDatabase
{
    class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Subject { get; set; }
        public int GroupId { get; set; }
    }

    class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int GroupId { get; set; }
    }

    class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int Rating { get; set; }
    }

    class ApplicationContext : DbContext
    {
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Grade> Grades => Set<Grade>();

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=university;Trusted_Connection=true;TrustServerCertificate=True");
            }
        }
    }

    internal class Program
    {
        static void PrintDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var groups = db.Groups.ToList();
                foreach (Group group in groups)
                {
                    Console.WriteLine($"Group ID: {group.Id}, Title: {group.Title}");
                }

                var teachers = db.Teachers.ToList();
                foreach (Teacher teacher in teachers)
                {
                    Console.WriteLine($"Teacher ID: {teacher.Id}, Name: {teacher.FirstName} {teacher.SecondName}, Subject: {teacher.Subject}, GroupID: {teacher.GroupId}");
                }

                var students = db.Students.ToList();
                foreach (Student student in students)
                {
                    Console.WriteLine($"Student ID: {student.Id}, Name: {student.FirstName} {student.SecondName}, GroupID: {student.GroupId}");
                }

                var grades = db.Grades.ToList();
                foreach (Grade grade in grades)
                {
                    Console.WriteLine($"Grade ID: {grade.Id}, StudentID: {grade.StudentId}, TeacherID: {grade.TeacherId}, Rating: {grade.Rating}");
                }
            }
        }
        static void Main(string[] args)
        {
            AddDB();
            RedactDB();
            PrintDB();
            DeleteDB();
        }

        static void AddDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Group g1 = new Group { Title = "AAB-31" }; 
                Group g2 = new Group { Title = "GGW-15" }; 
                db.Groups.Add(g1);
                db.Groups.Add(g2);

                Teacher t1 = new Teacher { FirstName = "John", SecondName = "Smith", Subject = "Math", GroupId = 1 }; 
                Teacher t2 = new Teacher { FirstName = "Emily", SecondName = "Johnson", Subject = "Physics", GroupId = 2 }; 
                db.Teachers.Add(t1);
                db.Teachers.Add(t2);

                Student s1 = new Student { FirstName = "Alice", SecondName = "Smith", GroupId = 1 }; 
                Student s2 = new Student { FirstName = "Michael", SecondName = "Brown", GroupId = 2 }; 
                db.Students.Add(s1);
                db.Students.Add(s2);

                Grade gr1 = new Grade { StudentId = 1, TeacherId = 1, Rating = 9 }; 
                Grade gr2 = new Grade { StudentId = 2, TeacherId = 2, Rating = 8 }; 
                db.Grades.Add(gr1);
                db.Grades.Add(gr2);

                db.SaveChanges();
            }
        }


        static void RedactDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Student? student = db.Students.FirstOrDefault();
                if (student != null)
                {
                    student.FirstName = "UpdatedName";
                    student.SecondName = "UpdatedSurname";
                    db.Students.Update(student);
                    db.SaveChanges();
                }
            }
        }

        static void DeleteDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Group? group = db.Groups.FirstOrDefault();
                if (group != null)
                {
                    db.Groups.Remove(group);
                    db.SaveChanges();
                }
            }
        }
    }
}