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

        public IEnumerable<Sermon> GetSermons()
        {
            return _sermons;
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
            _sermons.RemoveAll(s => s.Id == sermonId);
        }

        public void UpdateSermon(Sermon sermon)
        {
            var lookupSermon = _sermons.SingleOrDefault(s => s.Id == sermon.Id);
            if (lookupSermon != null)
            {
                sermon.Id = lookupSermon.Id;
                lookupSermon = sermon;
            }
        }

        public IEnumerable<Location> GetLocations()
        {
            return _locations;
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

        public void UpdateLocation(Location location)
        {
            var lookupLocation = _locations.SingleOrDefault(l => l.Id == location.Id);
            if (lookupLocation != null)
            {
                location.Id = lookupLocation.Id;
                lookupLocation = location;
            }
        }

        /// <summary>
        /// Retrieve all media in the repository.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Media> GetMedias()
        {
            return _medias;
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

        public void UpdateMedia(Media media)
        {
            var lookupMedia = _medias.SingleOrDefault(p => p.Id == media.Id);
            if (lookupMedia != null)
            {
                media.Id = lookupMedia.Id;
                lookupMedia = media;
            }
        }

        public IEnumerable<Preacher> GetPreachers()
        {
            return _preachers;
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

        public void UpdatePreacher(Preacher preacher)
        {
            var lookupPreacher = _preachers.SingleOrDefault(p => p.Id == preacher.Id);
            if (lookupPreacher != null)
            {
                preacher.Id = lookupPreacher.Id;
                lookupPreacher = preacher;
            }
        }

        public IEnumerable<Section> GetSections()
        {
            return _sections;
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

        public void UpdateSection(Section section)
        {
            var lookupSection = _sections.SingleOrDefault(s => s.Id == section.Id);
            if (lookupSection != null)
            {
                section.Id = lookupSection.Id;
                lookupSection = section;
            }
        }

        public IEnumerable<Series> GetSerieses()
        {
            return _serieses;
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

        public void UpdateSeries(Series series)
        {
            return;
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
