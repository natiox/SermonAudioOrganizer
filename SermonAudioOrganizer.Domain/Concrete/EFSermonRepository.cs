using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SermonAudioOrganizer.Domain
{
    /// <summary>
    /// Entity Framework Sermon Repository
    /// </summary>
    public class EFSermonRepository : ISermonRepository
    {
        private SermonContext context;

        public EFSermonRepository(SermonContext context)
        {
            this.context = context;
        }

        public IQueryable<Sermon> GetSermons()
        {
            return context.Sermons;
        }

        public Sermon GetSermonById(int sermonId)
        {
            return context.Sermons.Find(sermonId);
        }

        public void InsertSermon(Sermon sermon)
        {
            context.Sermons.Add(sermon);
        }

        public void DeleteSermon(int sermonId)
        {
            Sermon sermonToDelete = context.Sermons.Find(sermonId);
            if (sermonToDelete.SermonMedia != null)
            {
                foreach (var media in sermonToDelete.SermonMedia)
                {
                    context.Medias.Remove(media);
                } 
            }
            context.Sermons.Remove(sermonToDelete);
        }

        public IQueryable<Location> GetLocations()
        {
            return context.Locations;
        }

        public Location GetLocationById(int locationId)
        {
            return context.Locations.Find(locationId);
        }

        public void InsertLocation(Location location)
        {
            context.Locations.Add(location);
        }

        public void DeleteLocation(int locationId)
        {
            Location locationToDelete = context.Locations.Find(locationId);
            context.Locations.Remove(locationToDelete);
        }

        public IQueryable<Media> GetMedias()
        {
            return context.Medias;
        }

        public Media GetMediaById(int mediaId)
        {
            return context.Medias.Find(mediaId);
        }

        public void InsertMedia(Media media)
        {
            context.Medias.Add(media);
        }

        public void DeleteMedia(int mediaId)
        {
            Media mediaToDelete = context.Medias.Find(mediaId);
            context.Medias.Remove(mediaToDelete);
        }

        public IQueryable<Preacher> GetPreachers()
        {
            return context.Preachers;
        }

        public Preacher GetPreacherById(int preacherId)
        {
            return context.Preachers.Find(preacherId);
        }

        public void InsertPreacher(Preacher preacher)
        {
            context.Preachers.Add(preacher);
        }

        public void DeletePreacher(int preacherId)
        {
            Preacher preacherToDelete = context.Preachers.Find(preacherId);
            context.Preachers.Remove(preacherToDelete);
        }

        public IQueryable<Section> GetSections()
        {
            return context.Sections;
        }

        public Section GetSectionById(int sectionId)
        {
            return context.Sections.Find(sectionId);
        }

        public void InsertSection(Section section)
        {
            context.Sections.Add(section);
        }

        public void DeleteSection(int sectionId)
        {
            Section sectionToDelete = context.Sections.Find(sectionId);
            context.Sections.Remove(sectionToDelete);
        }

        public IQueryable<Series> GetSerieses()
        {
            return context.Serieses;
        }

        public Series GetSeriesById(int seriesId)
        {
            return context.Serieses.Find(seriesId);
        }

        public void InsertSeries(Series series)
        {
            context.Serieses.Add(series);
        }

        public void DeleteSeries(int seriesId)
        {
            Series seriesToDelete = context.Serieses.Find(seriesId);
            context.Serieses.Remove(seriesToDelete);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
