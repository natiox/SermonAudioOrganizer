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


        [DisplayName("Recording Date")]
        [DataType(DataType.Date)]
        public DateTime RecordingDate { get; set; }

        public string Topic { get; set; }

        public string Comment { get; set; }

        public Preacher SermonPreacher { get; set; }

        public Location SermonLocation { get; set; }

        /// <summary>
        /// i.e. Rightly Dividing the Gospels
        /// </summary>
        [DisplayName("Sermon Series")]
        public Series SermonSeries { get; set; }

        /// <summary>
        /// i.e. 1, 2, 3, etc.
        /// </summary>
        [DisplayName("Sermon Index")]
        public int? SeriesIndex { get; set; }

        /// <summary>
        /// a, b, c, etc.
        /// </summary>
        [DisplayName("Sermon Sub-index")]
        public char? SeriesSubIndex { get; set; }

        /// <summary>
        /// i.e. Sermon on the Mount
        /// </summary>
        [DisplayName("Sermon Section")]
        public Section SermonSection { get; set; }

        /// <summary>
        /// i.e. part 40 of Sermon on the Mount section
        /// </summary>
        [DisplayName("Sermon Index")]
        public int? SectionIndex { get; set; }

        /// <summary>
        /// mp3s, Powerpoints, PDFs, etc.
        /// </summary>
        [DisplayName("Sermon Media")]
        public List<Media> SermonMedia { get; set; }
    }
}

