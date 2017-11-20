using System;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new VidzyContext();
            context.AddVideo("Video 4", DateTime.Today, "Horror", (byte)Classification.Gold);
        }
    }
}
