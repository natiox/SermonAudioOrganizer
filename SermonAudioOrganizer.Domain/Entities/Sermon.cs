using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SermonAudioOrganizer.Domain
{
    public class Sermon
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime RecordingDate { get; set; }

        public string Topic { get; set; }

        public string Comment { get; set; }

        public Preacher SermonPreacher { get; set; }

        public Location SermonLocation { get; set; }

        /// <summary>
        /// i.e. Rightly Dividing the Gospels
        /// </summary>
        public Series SermonSeries { get; set; }

        /// <summary>
        /// i.e. 1, 2, 3, etc.
        /// </summary>
        public int? SeriesIndex { get; set; }

        /// <summary>
        /// a, b, c, etc.
        /// </summary>
        public char? SeriesSubIndex { get; set; }

        /// <summary>
        /// i.e. Sermon on the Mount
        /// </summary>
        public Section SermonSection { get; set; }

        /// <summary>
        /// i.e. part 40 of Sermon on the Mount section
        /// </summary>
        public int? SectionIndex { get; set; }

        /// <summary>
        /// mp3s, Powerpoints, PDFs, etc.
        /// </summary>
        public List<Media> SermonMedia { get; set; }
    }
}

