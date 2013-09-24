﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public DateTime RecordingDate { get; set; }

        public string Topic { get; set; }

        public string Comment { get; set; }

        public Preacher SermonPreacher { get; set; }

        public Location SermonLocation { get; set; }

        public Series SermonSeries { get; set; }

        [DisplayName("Series Installment")]
        public int SeriesInstallment { get; set; }

        public Section SermonSection { get; set; }

        [DisplayName("Section Index")]
        public int SectionIndex { get; set; }

        public List<Media> SermonMedia { get; set; }
    }
}