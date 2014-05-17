using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SermonAudioOrganizer.Domain
{
    public class MemSermonRepository : ISermonRepository
    {
        private List<Sermon> _sermons;
        private int nextSermonId;
        private List<Location> _locations;
        private int nextLocationId;
        private List<Media> _medias;
        private int nextMediaId;
        private List<Preacher> _preachers;
        private int nextPreacherId;
        private List<Series> _serieses;
        private int nextSeriesId;
        private List<Section> _sections;
        private int nextSectionId;

        public MemSermonRepository()
        {
            _sermons = new List<Sermon>();
            nextSermonId = 1;
            _locations = new List<Location>();
            nextLocationId = 1;
            _medias = new List<Media>();
            nextMediaId = 1;
            _preachers = new List<Preacher>();
            nextPreacherId = 1;
            _serieses = new List<Series>();
            nextSeriesId = 1;
            _sections = new List<Section>();
            nextSectionId = 1;
        }

        public IQueryable<Sermon> GetSermons()
        {
            return _sermons.AsQueryable();
        }

        public Sermon GetSermonById(int sermonId)
        {
            return _sermons.SingleOrDefault(s => s.Id == sermonId);
        }

        public void InsertSermon(Sermon sermon)
        {
            sermon.Id = nextSermonId;
            if (!_sermons.Contains(sermon))
                _sermons.Add(sermon);
            nextSermonId++;
        }

        public void DeleteSermon(int sermonId)
        {
            Sermon sermon = _sermons.Find(s => s.Id == sermonId);
            if (sermon.SermonMedia != null)
            {
                foreach (var media in sermon.SermonMedia)
                {
                    _medias.Remove(media);
                } 
            }
            _sermons.RemoveAll(s => s.Id == sermonId);
        }

        public IQueryable<Location> GetLocations()
        {
            return _locations.AsQueryable();
        }

        public Location GetLocationById(int locationId)
        {
            return _locations.SingleOrDefault(l => l.Id == locationId);
        }

        public void InsertLocation(Location location)
        {
            location.Id = nextLocationId;
            _locations.Add(location);
            nextLocationId++;
        }

        public void DeleteLocation(int locationId)
        {
            _locations.RemoveAll(l => l.Id == locationId);
        }

        /// <summary>
        /// Retrieve all media in the repository.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Media> GetMedias()
        {
            return _medias.AsQueryable();
        }

        public Media GetMediaById(int mediaId)
        {
            return _medias.SingleOrDefault(m => m.Id == mediaId);
        }

        public void InsertMedia(Media media)
        {
            media.Id = nextMediaId;
            _medias.Add(media);
            nextMediaId++;
        }

        public void DeleteMedia(int mediaId)
        {
            _medias.RemoveAll(m => m.Id == mediaId);
        }

        public IQueryable<Preacher> GetPreachers()
        {
            return _preachers.AsQueryable();
        }

        public Preacher GetPreacherById(int preacherId)
        {
            return _preachers.SingleOrDefault(p => p.Id == preacherId);
        }

        public void InsertPreacher(Preacher preacher)
        {
            preacher.Id = nextPreacherId;
            _preachers.Add(preacher);
            nextPreacherId++;
        }

        public void DeletePreacher(int preacherID)
        {
            _preachers.RemoveAll(p => p.Id == preacherID);
        }

        public IQueryable<Section> GetSections()
        {
            return _sections.AsQueryable();
        }

        public Section GetSectionById(int sectionId)
        {
            return _sections.SingleOrDefault(s => s.Id == sectionId);
        }

        public void InsertSection(Section section)
        {
            section.Id = nextSectionId;
            _sections.Add(section);
            nextSectionId++;
        }

        public void DeleteSection(int sectionID)
        {
            _sections.RemoveAll(s => s.Id == sectionID);
        }

        public IQueryable<Series> GetSerieses()
        {
            return _serieses.AsQueryable();
        }

        public Series GetSeriesById(int seriesId)
        {
            return _serieses.SingleOrDefault(s => s.Id == seriesId);
        }

        public void InsertSeries(Series series)
        {
            series.Id = nextSeriesId;
            _serieses.Add(series);
            nextSeriesId++;
        }

        public void DeleteSeries(int seriesID)
        {
            _serieses.RemoveAll(s => s.Id == seriesID);
        }

        public void Save()
        {
            return;
        }

        public void Dispose()
        {
            return;
        }
    }
}
