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


                SeriesIndex = sermon.SeriesIndex.ToString() + sermon.SeriesSubIndex.ToString();

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

        public DateTime RecordingDate { get; set; }

        public string Topic { get; set; }

        public string Comment { get; set; }

        //Location - dropdown
        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        //Preacher - dropdown
        public int PreacherId { get; set; }
        public IEnumerable<SelectListItem> Preachers { get; set; }

        //Series - dropdown
        public int SeriesId { get; set; }
        public IEnumerable<SelectListItem> Serieses { get; set; }

        //Sections - dropdown
        public int SectionId { get; set; }
        public IEnumerable<SelectListItem> Sections { get; set; }

        /// <summary>
        /// i.e. 1, 2, 3, etc.
        /// </summary>
        [DisplayName("Series Index")]
        public string SeriesIndex { get; set; }

        /// <summary>
        /// i.e. 1, 2, 3, etc.
        /// </summary>
        [DisplayName("Section Index")]
        public int SectionIndex { get; set; }

        /// <summary>
        /// mp3s, Powerpoints, PDFs, etc.
        /// </summary>
        public List<Media> SermonMedia { get; set; }
    }
}