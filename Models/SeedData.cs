using FinalExam.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FinalExamContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FinalExamContext>>()))
            {
                // look for any items
                if (context.Course.Any())
                {
                    return;   // DB has been seeded
                }

                // seed data for items
                context.Course.AddRange(
                    new Course
                    {
                        Name = "C# Fundamentals",
                        Semester = "Fall",
                        Year = 2022,
                        Grade = "A",
                        IsCompleted = true
                    },
                    new Course
                    {
                        Name = "Python Fundamentals",
                        Semester = "Winter",
                        Year = 2022,
                        Grade = "B",
                        IsCompleted = true
                    },
                    new Course
                    {
                        Name = "Database Fundamentals",
                        Semester = "Winter",
                        Year = 2022,
                        Grade = "A",
                        IsCompleted = true
                    },
                    new Course
                    {
                        Name = "Java Fundamentals",
                        Semester = "Spring",
                        Year = 2024,
                        Grade="null",
                        IsCompleted = false
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
