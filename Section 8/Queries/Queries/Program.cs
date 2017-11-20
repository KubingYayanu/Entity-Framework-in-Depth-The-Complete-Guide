
using System;
using System.Data.Entity;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddingObjects();
            //UpdatingObjects();
            //RemovingObjects();
        }

        private static void AddingObjects()
        {
            var context = new PlutoContext();

            // Method 1, Using an existing object in context
            //var authors = context.Authors.ToList();
            //var author = context.Authors.Single(a => a.Id == 1);

            // Method 3
            var author = new Author() { Id = 1, Name = "Kubing" };
            context.Authors.Attach(author);

            var course = new Course()
            {
                Name = "New Course",
                Description = "New Description",
                FullPrice = 19.95f,
                Level = 1,
                //Author = author // Method 1
                //AuthorId = 1 // Method 2, Using foreign key properties
                Author = author // Method 3, not recommend
            };

            context.Courses.Add(course);

            context.SaveChanges();
        }

        private static void UpdatingObjects()
        {
            var context = new PlutoContext();

            var course = context.Courses.Find(4); // Single(c => c.Id == 4)
            course.Name = "New Name";
            course.AuthorId = 2;

            context.SaveChanges();
        }

        private static void RemovingObjects()
        {
            var context = new PlutoContext();

            //var course = context.Courses.Find(6);
            //context.Courses.Remove(course);

            // Cascade on delete
            var author = context.Authors.Include(a => a.Courses).Single(a => a.Id == 2);
            context.Courses.RemoveRange(author.Courses);
            context.Authors.Remove(author);

            context.SaveChanges();
        }

        private static void ChangeTracker()
        {
            var context = new PlutoContext();

            // Add an object
            context.Authors.Add(new Author() { Name = "New Author"});

            // Update an object
            var author = context.Authors.Find(3);
            author.Name = "Updated";

            // Remove an object
            var another = context.Authors.Find(4);
            context.Authors.Remove(another);

            var entries = context.ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                entry.Reload(); // Reload Data from DB
                Console.WriteLine(entry.State);
            }
        }
    }
}
