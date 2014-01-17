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
        private List<Location> _locations;
        private List<Media> _medias;
        private List<Preacher> _preachers;
        private List<Series> _serieses;
        private List<Section> _sections;

        public MemSermonRepository()
        {
            _sermons = new List<Sermon>();
            _locations = new List<Location>();
            _medias = new List<Media>();
            _preachers = new List<Preacher>();
            _serieses = new List<Series>();
            _sections = new List<Section>();
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
            if (!_sermons.Contains(sermon))
                _sermons.Add(sermon);
        }

        public void DeleteSermon(int sermonId)
        {
            throw new NotImplementedException();

        }

        public void UpdateSermon(Sermon sermon)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetLocations()
        {
            return _locations;
        }

        public Location GetLocationById(int locationId)
        {
            throw new NotImplementedException();
        }

        public void InsertLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public void DeleteLocation(int locationId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Media> GetMedias()
        {
            throw new NotImplementedException();
        }

        public Media GetMediaById(int mediaId)
        {
            throw new NotImplementedException();
        }

        public void InsertMedia(Media media)
        {
            throw new NotImplementedException();
        }

        public void DeleteMedia(int mediaId)
        {
            throw new NotImplementedException();
        }

        public void UpdateMedia(Media media)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Preacher> GetPreachers()
        {
            throw new NotImplementedException();
        }

        public Preacher GetPreacherById(int preacherId)
        {
            throw new NotImplementedException();
        }

        public void InsertPreacher(Preacher preacher)
        {
            throw new NotImplementedException();
        }

        public void DeletePreacher(int preacherID)
        {
            throw new NotImplementedException();
        }

        public void UpdatePreacher(Preacher preacher)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Section> GetSections()
        {
            throw new NotImplementedException();
        }

        public Section GetSectionById(int sectionId)
        {
            throw new NotImplementedException();
        }

        public void InsertSection(Section section)
        {
            throw new NotImplementedException();
        }

        public void DeleteSection(int sectionID)
        {
            throw new NotImplementedException();
        }

        public void UpdateSection(Section section)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Series> GetSerieses()
        {
            throw new NotImplementedException();
        }

        public Series GetSeriesById(int seriesId)
        {
            throw new NotImplementedException();
        }

        public void InsertSeries(Series series)
        {
            throw new NotImplementedException();
        }

        public void DeleteSeries(int seriesID)
        {
            throw new NotImplementedException();
        }

        public void UpdateSeries(Series series)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
