﻿using Microsoft.EntityFrameworkCore;

namespace ADO.NET04_EntityFramework
{
    class Student
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public int GroupID { get; set; }
    }
    class ApplicationContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=univer;Trusted_Connection=true;TrustServerCertificate=True");
            }
        }
    }
    internal class Program
    {
        static void PrintDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var students = db.Students.ToList();
                foreach (Student student in students)
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName}, Surname: {student.SurName}, GroupID: {student.GroupID}.");
                }
            }
        }
        static void Main(string[] args)
        {

        }
        static void AddDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Student st1 = new Student { Id = 1, FirstName = "Alice", SurName = "Smith", GroupID = 1 };
                Student st2 = new Student { Id = 2, FirstName = "Michael", SurName = "Brown", GroupID = 2 };
                Student st3 = new Student { Id = 3, FirstName = "Jane", SurName = "Mendez", GroupID = 1 };
                db.Students.Add(st1);
                db.Students.Add(st2);
                db.Students.Add(st3);
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
                    student.FirstName = "John";
                    student.SurName = "Weaver";
                    db.Students.Update(student);
                    db.SaveChanges();
                }
            }
        }
        static void DeleteDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Student? student = db.Students.FirstOrDefault();
                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                }
            }

        }
    }
}