using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SermonAudioOrganizer.Domain
{
    public class Sermon : IEquatable<Sermon>
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayName("Recording Date")]
        [DataType(DataType.Date)]
        public DateTime RecordingDate { get; set; }

        public string Topic { get; set; }

        public string Comment { get; set; }

        public string Passages { get; set; }

        public virtual Preacher SermonPreacher { get; set; }

        public virtual Location SermonLocation { get; set; }

        /// <summary>
        /// i.e. Rightly Dividing the Gospels
        /// </summary>
        [DisplayName("Sermon Series")]
        public virtual Series SermonSeries { get; set; }

        /// <summary>
        /// i.e. 1, 2, 3a, 3b, etc.
        /// </summary>
        [DisplayName("Series Index")]
        [MaxLength(4)]
        public string SeriesIndex { get; set; }

        /// <summary>
        /// i.e. Sermon on the Mount
        /// </summary>
        [DisplayName("Sermon Section")]
        public virtual Section SermonSection { get; set; }

        /// <summary>
        /// i.e. part 40 of Sermon on the Mount section
        /// </summary>
        [DisplayName("Section Index")]
        public int? SectionIndex { get; set; }

        /// <summary>
        /// mp3s, Powerpoints, PDFs, etc.
        /// </summary>
        [DisplayName("Sermon Media")]
        public virtual IList<Media> SermonMedia { get; set; }

        public string GetMainFileName()
        {
            if (SermonMedia != null)
            {
                Media media = SermonMedia.SingleOrDefault(sm => sm.Name.ToLower().Contains("mp3"));
                if (media != null)
                    return media.Name;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }


        public bool Equals(Sermon other)
        {
            if (other == null)
                return false;

            return this.GetMainFileName() == other.GetMainFileName();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Sermon s = obj as Sermon;
            if (s == null)
                return false;
            else
                return Equals(s);
        }

        public override int GetHashCode()
        {
            string fileName = this.GetMainFileName();
            if (!string.IsNullOrEmpty(fileName))
                return this.GetMainFileName().GetHashCode();
            else
                return 0;
        }
    }
}

