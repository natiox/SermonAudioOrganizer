using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SermonAudioOrganizer.Domain
{
    public class SermonContext : DbContext, ISermonContext
    {
        public SermonContext()
            : base("SermonOrganizer")
        { }

        public DbSet<Sermon> Sermons { get; set; }
        public DbSet<Preacher> Preachers { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Series> Serieses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Location> Locations { get; set; }

        IQueryable<T> ISermonContext.Query<T>()
        {
            return Set<T>();
        }
    }
}
