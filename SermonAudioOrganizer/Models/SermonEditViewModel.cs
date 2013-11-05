using SermonAudioOrganizer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SermonAudioOrganizer.Models
{
    public class SermonEditViewModel
    {
        public SermonEditViewModel(Sermon sermon, IEnumerable<Location> locations, IEnumerable<Preacher> preachers,
            IEnumerable<Series> serieses, IEnumerable<Section> sections)
        {
            Locations = new SelectList(locations);

            Preachers = new SelectList(preachers);

            Serieses = new SelectList(serieses);

            Sections = new SelectList(sections);

            if (sermon.Id > 0)
            {
                Id = sermon.Id;
                Title = sermon.Title;
                RecordingDate = sermon.RecordingDate;
                Topic = sermon.Topic;
                Comment = sermon.Comment;

                LocationId = sermon.SermonLocation.Id;

                SeriesIndex = sermon.SeriesIndex;
                SeriesSubIndex = sermon.SeriesSubIndex;

                SectionIndex = sermon.SectionIndex;

                SermonMedia = sermon.SermonMedia;
                PreacherId = sermon.SermonPreacher.Id;
                SeriesId = sermon.SermonSeries.Id;
                SectionId = sermon.SermonSection.Id;
            }
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayName("Recording Date")]
        [DataType(DataType.Date)]
        public DateTime RecordingDate { get; set; }

        public string Topic { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        //Location - dropdown
        [DisplayName("Location")]
        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        //Preacher - dropdown
        [DisplayName("Preacher")]
        public int PreacherId { get; set; }
        public IEnumerable<SelectListItem> Preachers { get; set; }

        //Series - dropdown
        [DisplayName("Series (i.e. Rightly Dividing The Gospels)")]
        public int SeriesId { get; set; }
        public IEnumerable<SelectListItem> Serieses { get; set; }

        //Sections - dropdown
        [DisplayName("Section (i.e. Sermon on the Mount)")]
        public int SectionId { get; set; }
        public IEnumerable<SelectListItem> Sections { get; set; }

        /// <summary>
        /// i.e. 1, 2, 3, etc.
        /// </summary>
        [DisplayName("Series Index")]
        public int? SeriesIndex { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Sub-index not valid.")]
        [DisplayName("Series Sub-index (a, b, c, etc.)")]
        public char? SeriesSubIndex { get; set; }

        /// <summary>
        /// i.e. 1, 2, 3, etc.
        /// </summary>
        [DisplayName("Section Index (a, b, c, d, etc.)")]
        [MaxLength(1)]
        public int? SectionIndex { get; set; }

        /// <summary>
        /// mp3s, Powerpoints, PDFs, etc.
        /// </summary>
        //TODO: Figure out how to work out media.
        public string File { get; set; }
        public List<Media> SermonMedia { get; set; }
    }
}
