using System.Collections.Generic;

namespace Queries.Core.Domain
{
    public class Tag
    {
        public Tag()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
