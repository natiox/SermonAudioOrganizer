﻿using SermonAudioOrganizer.Domain;
using System;
namespace MediaScan
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            ISermonRepository sermonRepository;
            //sermonRepository = new MemSermonRepository();
            sermonRepository = new EFSermonRepository(new SermonContext());

            MediaScan mediaScan = new MediaScan(args[0], sermonRepository);

            foreach (var sermon in sermonRepository.GetSermons())
            {
                Console.WriteLine(string.Format("{0} by {1} {2} dated {3} in {4}", 
                    sermon.Title, sermon.SermonPreacher.FirstName, sermon.SermonPreacher.LastName, sermon.RecordingDate.ToShortDateString(), sermon.SermonLocation.Venue));
            }
            // Keep the console window open in debug mode.
            System.Console.ReadKey();
            return 0;
        }
    }
} 