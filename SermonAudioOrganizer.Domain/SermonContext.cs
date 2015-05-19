using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SermonAudioOrganizer.Domain
{
    public class SermonContext : DbContext
    {
        public SermonContext()
            : base("SermonAudioOrganizer")
        { }

        public virtual DbSet<Sermon> Sermons { get; set; }
        public virtual DbSet<Preacher> Preachers { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Series> Serieses { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
    }
}
