using SermonAudioOrganizer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaScan
{
    class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: MediaScan <mp3 path>");
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
                return 1;
            }
            SermonContext context = new SermonContext();
            MediaScan mediaScan = new MediaScan(args[0], context);

            Console.WriteLine("Existing sermons:");
            foreach (var sermon in context.Sermons)
            {
                Console.WriteLine(string.Format("{0} by {1} {2} dated {3} in {4}",
                    sermon.Title, sermon.SermonPreacher.FirstName, sermon.SermonPreacher.LastName, sermon.RecordingDate.ToShortDateString(), 
                    sermon.SermonLocation == null ? "missing venue" : sermon.SermonLocation.Venue));
            }

            Console.WriteLine("Scanning input folder");
            mediaScan.Scan();
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
            return 0;
        }
    }
}
