using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vidzy
{
    public class Genre
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<Video> Videos { get; set; }

        public Genre()
        {
            Videos = new Collection<Video>();
        }
    }
}