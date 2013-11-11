using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SermonAudioOrganizer.Domain
{
    public enum MediaType
    {
        MP3 = 0,
        PDF,
        PowerPoint,
        WAV,
        Word
    }

    public class Media
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public MediaType Type { get; set; }
    }
}
