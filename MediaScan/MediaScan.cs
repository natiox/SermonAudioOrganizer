using SermonAudioOrganizer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace MediaScan
{
    public class MediaScan
    {
        /// <summary>
        /// Scans for media files in the given directory and loads them into the repository.
        /// </summary>
        /// <param name="mediaDirectory"></param>
        /// <param name="repository"></param>
        public MediaScan(string mediaDirectory, ISermonRepository repository)
        {
            Location defaultLocation;

            var locations = repository.GetLocations();
            if (locations.Count() > 0)
            {
                defaultLocation = locations
                .Where(l => l.City == "Albuquerque" && l.Venue == "Church of Christ")
                .SingleOrDefault();
            }
            else
            {
                defaultLocation = new Location() { City = "Albuquerque", State = "NM", Venue = "Church of Christ" };
                repository.InsertLocation(defaultLocation);
            }


            foreach (var filePath in Directory.GetFiles(mediaDirectory))
            {
                string fileName = Path.GetFileName(filePath);
                string title = string.Empty;

                //If media is already in system, don't bother
                if (!repository.GetMedias().Any(m => m.Name == fileName))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Media media = new Media(fileName);

                    var underscoreSplitFileName = Path.GetFileNameWithoutExtension(filePath).Split(new char[] { '_' });

                    if (underscoreSplitFileName.Count() < 2)
                    {
                        //TODO: Log too few elements in filename
                        continue;
                    }

                    //Splits on case change
                    //TODO: Also split from letters to numbers or numbers to letters
                    Regex regex = new Regex(@"
                        (?<=[A-Z])(?=[A-Z][a-z]) |
                         (?<=[^A-Z])(?=[A-Z]) |
                         (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

                    var preacherName = regex.Replace(underscoreSplitFileName[0], " ").Split(new char[] { ' ' });
                    string firstName = string.Empty;
                    string lastName = string.Empty;

                    if (preacherName.Count() == 2)
                    {
                        firstName = preacherName[0];
                        lastName = preacherName[1];
                    }
                    else if (preacherName.Count() == 1)
                    {
                        firstName = preacherName[0];
                    }
                    Preacher preacher;
                    if (string.IsNullOrEmpty(lastName))
                    {
                        preacher = repository.GetPreachers().Where(p => p.FirstName == firstName).SingleOrDefault();
                    }
                    else
                    {
                        preacher = repository.GetPreachers().Where(p => p.FirstName == firstName && p.LastName == lastName).SingleOrDefault();
                    }

                    if (preacher == null)
                    {
                        preacher = new Preacher() { FirstName = firstName, LastName = lastName };
                        repository.InsertPreacher(preacher);
                    }

                    //TODO: Try to parse out passages from name
                    //TODO: Case where filename isn't preacher_title
                    foreach (string word in underscoreSplitFileName.Skip(1))
                    {
                        title += regex.Replace(word, " ") + " ";
                    }

                    

                    Sermon sermon = new Sermon()
                    {
                        Title = title,
                        RecordingDate = fileInfo.CreationTime,
                        SermonPreacher = preacher,
                        SermonLocation = defaultLocation,
                        SermonMedia = new List<Media>() { media }
                    };

                    repository.InsertSermon(sermon);
                }
            }
            repository.Save();
        }
    }
}
