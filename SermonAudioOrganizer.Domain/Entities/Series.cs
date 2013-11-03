using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SermonAudioOrganizer.Domain
{
    public class Series
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Make "N/A" or "stand-alone" one of the choices, or have a radio button that chooses between serial and stand-alone
        /// </summary>
        public string Title { get; set; }
    }
}
