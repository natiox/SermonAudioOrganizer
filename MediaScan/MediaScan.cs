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
        private SermonContext _context;
        private string _mediaDirectory;

        public MediaScan(string mediaDirectory, SermonContext context)
        {
            _context = context;
            _mediaDirectory = mediaDirectory;
        }

        /// <summary>
        /// Scans for media files in the given directory and loads them into the repository.
        /// </summary>
        /// <param name="mediaDirectory"></param>
        /// <param name="repository"></param>
        public void Scan()
        {
            Location defaultLocation;

            var locations = _context.Locations;
            if (locations.Any())
            {
                defaultLocation = locations
                .Where(l => l.City == "Albuquerque" && l.Venue == "Church of Christ on Vermont Street")
                .FirstOrDefault();
            }
            else
            {
                defaultLocation = new Location() { City = "Albuquerque", State = "NM", Venue = "Church of Christ on Vermont Street" };
                _context.Locations.Add(defaultLocation);
                _context.SaveChanges();
            }

            foreach (var filePath in Directory.GetFiles(_mediaDirectory))
            {
                string fileName = Path.GetFileName(filePath);
                string title = string.Empty;

                //If media is already in system, don't bother
                if (!_context.Medias.Any(m => m.Name == fileName))
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
                        (?<=[A-Za-z])(?=[^A-Za-z]) |
                        (?<=[^A-Za-z])(?=[A-Za-z])", RegexOptions.IgnorePatternWhitespace);

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
                    //Find the preacher if it exists.

                    if (string.IsNullOrEmpty(lastName))
                    {
                        preacher = _context.Preachers.Where(p => p.FirstName == firstName).FirstOrDefault();
                    }
                    else
                    {
                        preacher = _context.Preachers.Where(p => p.FirstName == firstName && p.LastName == lastName).FirstOrDefault();
                    }

                    if (preacher == null)
                    {
                        preacher = new Preacher() { FirstName = firstName, LastName = lastName };
                        _context.Preachers.Add(preacher);
                        //Save preacher for use in next round
                        _context.SaveChanges();
                    }

                    //TODO: Try to parse out passages from name
                    //TODO: Case where filename isn't preacher_title
                    foreach (string word in underscoreSplitFileName.Skip(1))
                    {
                        title += regex.Replace(word, " ") + " ";
                    }

                    title = title.Trim();

                    Sermon sermon = new Sermon()
                    {
                        Title = title,
                        RecordingDate = fileInfo.CreationTime,
                        SermonPreacher = preacher,
                        SermonLocation = defaultLocation,
                        SermonMedia = new List<Media>() { media }
                    };

                    _context.Sermons.Add(sermon);
                }
            }
            _context.SaveChanges();
        }
    }
}
