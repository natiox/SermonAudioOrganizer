using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SermonAudioOrganizer.Domain
{
    public class SermonRepository : ISermonRepository
    {
        private SermonContext context;

        public SermonRepository(SermonContext context)
        {
            this.context = context;
        }

        public IEnumerable<Sermon> GetSermons()
        {
            return context.Sermons.ToList();
            //if (context.Sermons == null)
            //{
            //    return null;
            //}
            //else
            //{
            //    return context.Sermons.ToList();
            //}
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
            context.Sermons.Remove(sermonToDelete);
        }

        public void UpdateSermon(Sermon sermon)
        {
            context.Entry(sermon).State = System.Data.EntityState.Modified;
        }


        public IEnumerable<Location> GetLocations()
        {
            return context.Locations.ToList();
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

        public void UpdateLocation(Location location)
        {
            context.Entry(location).State = System.Data.EntityState.Modified;
        }


        public IEnumerable<Media> GetMedias()
        {
            return context.Medias.ToList();
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

        public void UpdateMedia(Media media)
        {
            context.Entry(media).State = System.Data.EntityState.Modified;
        }


        public IEnumerable<MediaType> GetMediaTypes()
        {
            return context.MediaTypes.ToList();
        }

        public MediaType GetMediaTypeById(int mediaTypeId)
        {
            return context.MediaTypes.Find(mediaTypeId);
        }

        public void InsertMediaType(MediaType mediaType)
        {
            context.MediaTypes.Add(mediaType);
        }

        public void DeleteMediaType(int mediaTypeId)
        {
            MediaType mediaTypeToDelete = context.MediaTypes.Find(mediaTypeId);
            context.MediaTypes.Remove(mediaTypeToDelete);
        }

        public void UpdateMediaType(MediaType mediaType)
        {
            context.Entry(mediaType).State = System.Data.EntityState.Modified;
        }


        public IEnumerable<Preacher> GetPreachers()
        {
            return context.Preachers.ToList();
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

        public void UpdatePreacher(Preacher preacher)
        {
            context.Entry(preacher).State = System.Data.EntityState.Modified;
        }


        public IEnumerable<Section> GetSections()
        {
            return context.Sections.ToList();
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

        public void UpdateSection(Section section)
        {
            context.Entry(section).State = System.Data.EntityState.Modified;
        }


        public IEnumerable<Series> GetSerieses()
        {
            return context.Serieses.ToList();
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

        public void UpdateSeries(Series series)
        {
            context.Entry(series).State = System.Data.EntityState.Modified;
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
