
using System;
using System.Data.Entity;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            // Lazy Loading - Don't use in web application!!
            //var courses = context.Courses.Single(c => c.Id == 2);

            //foreach (var tag in courses.Tags)
            //    Console.WriteLine(tag.Name);


            // Eager Loading
            var courses = context.Courses.Include(c => c.Author).ToList();

            foreach (var course in courses)
                Console.WriteLine($"{course.Name} by {course.Author.Name}");

            // Eager Loading - Multiple Levels
            // For ingle properties
            //context.Courses.Include(c => c.Author.Address);

            // For collection properties
            //context.Courses.Include(c => c.Tags.Select(t => t.Moderator));


            // Explicit Loading - Singleton
            var author = context.Authors.Single(a => a.Id == 1);

            // MSDN way
            context.Entry(author).Collection(a => a.Courses).Query().Where(c => c.FullPrice == 0).Load();

            // Mosh way
            context.Courses.Where(c => c.AuthorId == author.Id && c.FullPrice == 0).Load();

            foreach (var course in author.Courses)
                Console.WriteLine($"{course.Name}");

            // Explicit Loading - Multiple
            var authors = context.Authors.ToList();
            var authorIds = authors.Select(a => a.Id);

            context.Courses.Where(c => authorIds.Contains(c.AuthorId) && c.FullPrice == 0).Load();
        }
    }
}
