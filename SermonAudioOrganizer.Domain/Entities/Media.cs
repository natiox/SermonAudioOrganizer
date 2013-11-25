using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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

        public Media(string fileName)
        {
            Name = Path.GetFileName(fileName);
            switch (Path.GetExtension(Name).ToLower())
            {
                case "mp3":
                    Type = MediaType.MP3;
                    break;
                case "pdf":
                    Type = MediaType.PDF;
                    break;
                case "pptx":
                case "ppt":
                    Type = MediaType.PowerPoint;
                    break;
                case "wav":
                    Type = MediaType.WAV;
                    break;
                case "doc":
                case "docx":
                    Type = MediaType.Word;
                    break;
                default:
                    break;
            }
        }
    }
}
