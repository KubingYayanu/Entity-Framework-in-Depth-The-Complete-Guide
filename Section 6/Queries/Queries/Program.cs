
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            //LinqSyntax();
            //LinqExtensionMethod();
            //AdditionalExtensionMethod();
            //DefferedExecution();
            IQueryable();

            Console.ReadKey();
        }

        private static void LinqSyntax()
        {
            var context = new PlutoContext();

            // 1.LINQ syntax
            var query =
                from c in context.Courses
                where c.Name.Contains("c#")
                orderby c.Name
                select c;

            //foreach (var course in query)
            //    Console.WriteLine(course.Name);

            // 2.Extension methods
            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            //foreach (var course in courses)
            //    Console.WriteLine(course.Name);

            // 3.Group
            var groups =
                from c in context.Courses
                group c by c.Level into g
                select g;

            //foreach (var group in groups)
            //{
            //    //Console.WriteLine(group.Key);
            //    //foreach (var course in group)
            //    //    Console.WriteLine($"\t{course.Name}");

            //    Console.WriteLine($"{group.Key} ({group.Count()})");
            //}

            // 4.Inner Join
            var innerJoin =
                from c in context.Courses
                join a in context.Authors on c.AuthorId equals a.Id
                select new { CourseName = c.Name, AuthorName = a.Name };

            // 5.Group join (like left join in sql)
            var groupJoin =
                from a in context.Authors
                join c in context.Courses on a.Id equals c.AuthorId into g
                select new { AuthorName = a.Name, Courses = g.Count() };

            //foreach (var x in groupJoin)
            //    Console.WriteLine($"{x.AuthorName} ({x.Courses})");

            // 6.Cross Join
            var crossJoin =
                from a in context.Authors
                from c in context.Courses
                select new { AuthorName = a.Name, CourseName = c.Name };

            //foreach (var x in crossJoin)
            //    Console.WriteLine($"{x.AuthorName} - {x.CourseName}");
        }

        private static void LinqExtensionMethod()
        {
            var context = new PlutoContext();

            var tags = context.Courses
                .Where(c => c.Level == 1)
                .OrderByDescending(c => c.Name)
                .ThenByDescending(c => c.Level)
                .SelectMany(c => c.Tags)
                .Distinct();

            //foreach (var t in tags)
            //    Console.WriteLine(t.Name);

            // Group
            var groups = context.Courses.GroupBy(c => c.Level);

            foreach (var group in groups)
            {
                Console.WriteLine($"Key: {group.Key}");

                foreach (var course in group)
                {
                    Console.WriteLine($"\t{course.Name}");
                }
            }

            // Inner Join
            var innerJoin = context.Courses.Join(context.Authors
                , c => c.AuthorId
                , a => a.Id
                , (course, author) => new
                    {
                        CourseName = course.Name,
                        AuthorName = author.Name
                    });

            // Group Join
            var groupJoin = context.Authors.GroupJoin(context.Courses
                , a => a.Id
                , c => c.AuthorId
                , (author, courses) => new
                    {
                        AuthorName = author.Name,
                        Courses = courses
                    });

            // Cross Join
            var crossJoin = context.Authors.SelectMany(a => context.Courses
                , (author, course) => new
                    {
                        AuthorName = author.Name,
                        CourseName = course.Name
                    });

        }

        private static void AdditionalExtensionMethod()
        {
            var context = new PlutoContext();

            // Partition
            var partition = context.Courses.Skip(10).Take(10);

            // Element Operators
            var first = context.Courses.OrderBy(c => c.Level).FirstOrDefault(c => c.FullPrice > 100);
            var last = context.Courses.LastOrDefault(c => c.FullPrice > 100);
            var single = context.Courses.SingleOrDefault(c => c.Id == 1);

            // Quantifying
            var all = context.Courses.All(c => c.FullPrice > 10);
            var any = context.Courses.Any(c => c.Level == 1);

            // Aggregating
            var count = context.Courses.Count();
            var max = context.Courses.Max(c => c.FullPrice);
            var min = context.Courses.Min(c => c.FullPrice);
            var average = context.Courses.Average(c => c.FullPrice);
        }

        private static void DefferedExecution()
        {
            var context = new PlutoContext();

            var courses = context.Courses.Where(c => c.Level == 1).OrderBy(c => c.Name);
            var filtered = courses.Where(c => c.Level == 1);
            var sorted = filtered.OrderBy(c => c.Name);

            // Query executed when
            // 。Iterating over query variable
            // 。Calling ToList, ToArray, ToDictionary
            // 。Calling First, Last, Single, Count, Max, Min, Average
            foreach (var c in courses)
            {
                Console.WriteLine(c.Name);
            }
        }

        private static void IQueryable()
        {
            var context = new PlutoContext();

            // In SQL Server Profiler, there is no "where clause"
            // The filtered will excute in memory
            IEnumerable<Course> courses = context.Courses;

            // In SQL Server Profiler, there has "where clause"
            //IQueryable<Course> courses = context.Courses;
            var filtered = courses.Where(C => C.Level == 1);

            foreach (var course in filtered)
                Console.WriteLine(course.Name);


        }
    }
}
