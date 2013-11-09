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
        public SermonEditViewModel()
        {

        }

        public SermonEditViewModel(Sermon sermon, IEnumerable<Location> locations, IEnumerable<Preacher> preachers,
            IEnumerable<Series> serieses, IEnumerable<Section> sections)
        {
            Locations = new SelectList(from l in locations
                                       select new { Id = l.Id, LocationName = string.Format("{0} - {1}, {2}", l.Venue, l.City, l.State) },
                                           "Id",
                                           "LocationName",
                                           (sermon.SermonLocation != null) ? sermon.SermonLocation.Id : 0);

            Preachers = new SelectList(from p in preachers
                                       select new { Id = p.Id, PreacherName = string.Format("{0} {1}", p.FirstName, p.LastName) },
                                       "Id",
                                       "PreacherName",
                                       (sermon.SermonPreacher != null) ? sermon.SermonPreacher.Id : 0);

            Serieses = new SelectList(serieses, "Id", "Title", (sermon.SermonSeries != null) ? sermon.SermonSeries.Id : 0);

            Sections = new SelectList(sections, "Id", "Title", (sermon.SermonSection != null) ? sermon.SermonSection.Id : 0);

            //Edit existing sermon
            if (sermon.Id > 0)
            {
                Id = sermon.Id;
                Title = sermon.Title;

                //TODO: This isn't getting set.
                RecordingDate = sermon.RecordingDate.Date;
                Topic = sermon.Topic;
                Comment = sermon.Comment;

                SeriesIndex = sermon.SeriesIndex;
                SeriesSubIndex = sermon.SeriesSubIndex;

                SectionIndex = sermon.SectionIndex;

                SermonMedia = sermon.SermonMedia;

                if (sermon.SermonLocation != null)
                    LocationId = sermon.SermonLocation.Id;

                if (sermon.SermonPreacher != null)
                    PreacherId = sermon.SermonPreacher.Id;

                if (sermon.SermonSeries != null)
                    SeriesId = sermon.SermonSeries.Id;

                if (sermon.SermonSection != null)
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
        [DisplayName("Section Index")]
        public int? SectionIndex { get; set; }

        /// <summary>
        /// mp3s, Powerpoints, PDFs, etc.
        /// </summary>
        //TODO: Figure out how to work out media.
        public string File { get; set; }
        public List<Media> SermonMedia { get; set; }
    }
}
