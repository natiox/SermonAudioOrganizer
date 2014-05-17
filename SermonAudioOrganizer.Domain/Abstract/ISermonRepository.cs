using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SermonAudioOrganizer.Domain
{
    public interface ISermonRepository : IDisposable
    {
        IQueryable<Sermon> GetSermons();
        Sermon GetSermonById(int sermonId);
        void InsertSermon(Sermon sermon);
        void DeleteSermon(int sermonId);

        IQueryable<Location> GetLocations();
        Location GetLocationById(int locationId);
        void InsertLocation(Location location);
        void DeleteLocation(int locationId);

        IQueryable<Media> GetMedias();
        Media GetMediaById(int mediaId);
        void InsertMedia(Media media);
        void DeleteMedia(int mediaId);

        IQueryable<Preacher> GetPreachers();
        Preacher GetPreacherById(int preacherId);
        void InsertPreacher(Preacher preacher);
        void DeletePreacher(int preacherID);

        IQueryable<Section> GetSections();
        Section GetSectionById(int sectionId);
        void InsertSection(Section section);
        void DeleteSection(int sectionID);

        IQueryable<Series> GetSerieses();
        Series GetSeriesById(int seriesId);
        void InsertSeries(Series series);
        void DeleteSeries(int seriesID);

        void Save();
    }
}
