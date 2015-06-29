using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SermonAudioOrganizer.Domain;
using SermonAudioOrganizer.Models;

using PagedList;

namespace SermonAudioOrganizer.Controllers
{
    public class SermonController : Controller
    {
        private SermonContext _sermonContext;

        public SermonController()
        {
            _sermonContext = new SermonContext();
        }

        public ActionResult AddMedia(int sermonId = 0)
        {
            AddMediaViewModel addMediaViewModel = new AddMediaViewModel() { SermonId = sermonId, AllMediaList = _sermonContext.Medias.ToList() };
            return View(addMediaViewModel);
            //TODO: Need AddMediaConfirm
        }

        public ActionResult AddMediaToSermon(int mediaId = 0, int sermonId = 0)
        {
            AddMediaConfirmViewModel addMediaConfirmViewModel = new AddMediaConfirmViewModel() { SermonId = sermonId, MediaId = mediaId };
            return View(addMediaConfirmViewModel);
        }

        [HttpPost]
        public ActionResult AddMediaToSermon(AddMediaConfirmViewModel addMediaConfirmViewModel)
        {
            var media = _sermonContext.Medias.Find(addMediaConfirmViewModel.MediaId);
            var sermon = _sermonContext.Sermons.Find(addMediaConfirmViewModel.SermonId);
            sermon.SermonMedia.Add(media);
            _sermonContext.SaveChanges();
            return RedirectToAction("Edit", new { id = addMediaConfirmViewModel.SermonId });
        }
        
        [HttpPost]
        public ActionResult RemoveMediaFromSermon(int mediaId = 0, int sermonId = 0)
        {
            var media = _sermonContext.Medias.Find(mediaId);
            _sermonContext.Sermons.Find(sermonId).SermonMedia.Remove(media);
            return View();
        }

        //
        // GET: /Sermon/
        public ActionResult Index(int? page, int? preacherId, int? seriesId, int? sectionId, int? locationId, string searchTitle = "")
        {
            var sermons = from s in _sermonContext.Sermons
                          select s;

            if (!string.IsNullOrEmpty(searchTitle))
                sermons = sermons.Where(s => s.Title.Contains(searchTitle));

            if (preacherId != null)
                sermons = sermons.Where(s => s.SermonPreacher.Id == preacherId);

            if (seriesId != null)
                sermons = sermons.Where(s => s.SermonSeries.Id == seriesId);

            if (sectionId != null)
                sermons = sermons.Where(s => s.SermonSection.Id == sectionId);

            if (locationId != null)
                sermons = sermons.Where(s => s.SermonLocation.Id == locationId);

            sermons = sermons.OrderByDescending(s => s.RecordingDate);
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfSermons = sermons.ToPagedList(pageNumber, 25); // will only contain 25 products max because of the pageSize

            ViewBag.preacherId = preacherId;
            ViewBag.searchTitle = searchTitle;
            ViewBag.seriesId = seriesId;
            ViewBag.sectionId = sectionId;
            ViewBag.locationId = locationId;
            return View(onePageOfSermons);
        }

        //
        // TODO: GET: /Sermon/Details/5
        public ActionResult Details(int id = 0)
        {
            Sermon sermon = _sermonContext.Sermons.Find(id);
            if (sermon == null)
            {
                return HttpNotFound();
            }
            return View(sermon);
        }

        //
        // GET: /Sermon/Create
        public ActionResult Create()
        {
            Sermon sermon = new Sermon();

            SermonEditViewModel sermonEditViewModel = new SermonEditViewModel(sermon, _sermonContext.Locations, _sermonContext.Preachers,
                _sermonContext.Serieses, _sermonContext.Sections);
            return View(sermonEditViewModel);
        }

        //
        // POST: /Sermon/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SermonEditViewModel sermonViewModel)
        {
            Sermon sermon = new Sermon()
            {
                Comment = sermonViewModel.Comment,
                Passages = sermonViewModel.Passages,
                RecordingDate = sermonViewModel.RecordingDate,
                SectionIndex = sermonViewModel.SectionIndex,
                SeriesIndex = sermonViewModel.SeriesIndex,
                SermonLocation = (sermonViewModel.LocationId != null) ? _sermonContext.Locations.Find(sermonViewModel.LocationId.Value) : null,
                SermonPreacher = (sermonViewModel.PreacherId != null) ? _sermonContext.Preachers.Find(sermonViewModel.PreacherId.Value) : null,
                SermonSeries = (sermonViewModel.SeriesId != null) ? _sermonContext.Serieses.Find(sermonViewModel.SeriesId.Value) : null,
                SermonSection = (sermonViewModel.SectionId != null) ? _sermonContext.Sections.Find(sermonViewModel.SectionId.Value) : null,
                Title = sermonViewModel.Title,
                Topic = sermonViewModel.Topic
                // TODO: SermonMedia = _sermonContext.MediaById(sermonViewModel.SermonMedia.Media
            };

            if (ModelState.IsValid)
            {
                _sermonContext.Sermons.Add(sermon);
                _sermonContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sermon);
        }

        //
        // GET: /Sermon/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Sermon sermon = _sermonContext.Sermons.Find(id);
            if (sermon == null)
            {
                return HttpNotFound();
            }

            SermonEditViewModel sermonEditViewModel = new SermonEditViewModel(sermon, _sermonContext.Locations, _sermonContext.Preachers,
                _sermonContext.Serieses, _sermonContext.Sections);
            return View(sermonEditViewModel);
        }

        //
        // POST: /Sermon/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SermonEditViewModel sermonViewModel)
        {
            Sermon sermon = _sermonContext.Sermons.Find(sermonViewModel.Id);

            if (sermon.Title != sermonViewModel.Title)
                sermon.Title = sermonViewModel.Title;

            if (sermon.Topic != sermonViewModel.Topic)
                sermon.Topic = sermonViewModel.Topic;

            if (sermon.Comment != sermonViewModel.Comment)
                sermon.Comment = sermonViewModel.Comment;

            if (sermon.Passages != sermonViewModel.Passages)
                sermon.Passages = sermonViewModel.Passages;

            if (sermon.RecordingDate != sermonViewModel.RecordingDate)
                sermon.RecordingDate = sermonViewModel.RecordingDate;

            if (sermon.SectionIndex != sermonViewModel.SectionIndex)
                sermon.SectionIndex = sermonViewModel.SectionIndex;

            if (sermon.SeriesIndex != sermonViewModel.SeriesIndex)
                sermon.SeriesIndex = sermonViewModel.SeriesIndex;

            if (((sermon.SermonLocation != null) ? sermon.SermonLocation.Id : 0) != sermonViewModel.LocationId)
                sermon.SermonLocation = (sermonViewModel.LocationId != null) ? _sermonContext.Locations.Find(sermonViewModel.LocationId.Value) : null;

            if (((sermon.SermonPreacher != null) ? sermon.SermonPreacher.Id : 0) != sermonViewModel.PreacherId)
               sermon.SermonPreacher = (sermonViewModel.PreacherId != null) ? _sermonContext.Preachers.Find(sermonViewModel.PreacherId.Value) : null;

            if (((sermon.SermonSeries != null) ? sermon.SermonSeries.Id : 0) != sermonViewModel.SeriesId)
                sermon.SermonSeries = (sermonViewModel.SeriesId != null) ? _sermonContext.Serieses.Find(sermonViewModel.SeriesId.Value) : null;

            if (((sermon.SermonSection != null) ? sermon.SermonSection.Id : 0) != sermonViewModel.SectionId)
                sermon.SermonSection = (sermonViewModel.SectionId != null) ? _sermonContext.Sections.Find(sermonViewModel.SectionId.Value) : null;

                //TODO: SermonMedia = _sermonContext.MediaById(sermonViewModel.SermonMedia.Media

            if (ModelState.IsValid)
            {
                _sermonContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sermon);
        }

        //
        // GET: /Sermon/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Sermon sermon = _sermonContext.Sermons.Find(id);
            if (sermon == null)
            {
                return HttpNotFound();
            }
            return View(sermon);
        }

        //
        // POST: /Sermon/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sermon sermon = _sermonContext.Sermons.Find(id);
            sermon.SermonMedia = null;
            _sermonContext.Sermons.Attach(sermon);
            _sermonContext.Sermons.Remove(sermon);
            _sermonContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}