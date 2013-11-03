using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SermonAudioOrganizer.Domain
{
    public interface ISermonRepository : IDisposable
    {
        IEnumerable<Sermon> GetSermons();
        Sermon GetSermonById(int sermonId);
        void InsertSermon(Sermon sermon);
        void DeleteSermon(int sermonId);
        void UpdateSermon(Sermon sermon);

        IEnumerable<Location> GetLocations();
        Location GetLocationById(int locationId);
        void InsertLocation(Location location);
        void DeleteLocation(int locationId);
        void UpdateLocation(Location location);

        IEnumerable<Media> GetMedias();
        Media GetMediaById(int mediaId);
        void InsertMedia(Media media);
        void DeleteMedia(int mediaId);
        void UpdateMedia(Media media);

        IEnumerable<MediaType> GetMediaTypes();
        MediaType GetMediaTypeById(int mediaTypeId);
        void InsertMediaType(MediaType mediaType);
        void DeleteMediaType(int mediaTypeID);
        void UpdateMediaType(MediaType mediaType);

        IEnumerable<Preacher> GetPreachers();
        Preacher GetPreacherById(int preacherId);
        void InsertPreacher(Preacher preacher);
        void DeletePreacher(int preacherID);
        void UpdatePreacher(Preacher preacher);

        IEnumerable<Section> GetSections();
        Section GetSectionById(int sectionId);
        void InsertSection(Section section);
        void DeleteSection(int sectionID);
        void UpdateSection(Section section);

        IEnumerable<Series> GetSerieses();
        Series GetSeriesById(int seriesId);
        void InsertSeries(Series series);
        void DeleteSeries(int seriesID);
        void UpdateSeries(Series series);

        void Save();
    }
}
