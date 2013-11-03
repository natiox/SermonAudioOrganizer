using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SermonAudioOrganizer.Domain
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Venue { get; set; }
    }
}
