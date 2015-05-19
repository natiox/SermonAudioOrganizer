using SermonAudioOrganizer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SermonAudioOrganizer.Models
{
    public class AddMediaViewModel
    {
        public int SermonId { get; set; }

        [DisplayName("All Media")]
        public IList<Media> AllMediaList { get; set; }
    }
}