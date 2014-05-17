using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SermonAudioOrganizer.Domain;
using SermonAudioOrganizer.Models;

namespace SermonAudioOrganizer.Controllers
{
    public class SermonController : Controller
    {
        private ISermonRepository repository;

        public SermonController()
        {
            repository = new EFSermonRepository(new SermonContext());
        }

        public ActionResult AddMedia(int sermonId = 0)
        {
            AddMediaViewModel addMediaViewModel = new AddMediaViewModel() { SermonId = sermonId, AllMediaList = repository.GetMedias().ToList() };
            return View(addMediaViewModel);
        }

        public ActionResult AddMediaToSermon(int mediaId = 0, int sermonId = 0)
        {
            AddMediaConfirmViewModel addMediaConfirmViewModel = new AddMediaConfirmViewModel() { SermonId = sermonId, MediaId = mediaId };
            return View(addMediaConfirmViewModel);
        }

        [HttpPost]
        public ActionResult AddMediaToSermon(AddMediaConfirmViewModel addMediaConfirmViewModel)
        {
            repository.GetSermonById(addMediaConfirmViewModel.SermonId).SermonMedia.Add(repository.GetMediaById(addMediaConfirmViewModel.MediaId));
            return RedirectToAction("Edit", new { id = addMediaConfirmViewModel.SermonId });
        }
        
        [HttpPost]
        public ActionResult RemoveMediaFromSermon(int mediaId = 0, int sermonId = 0)
        {
            repository.GetSermonById(sermonId).SermonMedia.Add(repository.GetMediaById(mediaId));
            return View();
        }

        //
        // GET: /Sermon/

        public ActionResult Index()
        {
            return View(repository.GetSermons());
        }

        //
        // GET: /Sermon/Details/5

        public ActionResult Details(int id = 0)
        {
            //TODO: Need search of various types.
            Sermon sermon = repository.GetSermonById(id);
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

            SermonEditViewModel sermonEditViewModel = new SermonEditViewModel(sermon, repository.GetLocations(), repository.GetPreachers(),
                repository.GetSerieses(), repository.GetSections());
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
                SermonLocation = (sermonViewModel.LocationId != null) ? repository.GetLocationById(sermonViewModel.LocationId.Value) : null,
                SermonPreacher = (sermonViewModel.PreacherId != null) ? repository.GetPreacherById(sermonViewModel.PreacherId.Value) : null,
                SermonSeries = (sermonViewModel.SeriesId != null) ? repository.GetSeriesById(sermonViewModel.SeriesId.Value) : null,
                SermonSection = (sermonViewModel.SectionId != null) ? repository.GetSectionById(sermonViewModel.SectionId.Value) : null,
                Title = sermonViewModel.Title,
                Topic = sermonViewModel.Topic
                // TODO: SermonMedia = repository.GetMediaById(sermonViewModel.SermonMedia.Media
            };

            if (ModelState.IsValid)
            {
                repository.InsertSermon(sermon);
                repository.Save();
                return RedirectToAction("Index");
            }

            return View(sermon);
        }

        //
        // GET: /Sermon/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Sermon sermon = repository.GetSermonById(id);
            if (sermon == null)
            {
                return HttpNotFound();
            }

            SermonEditViewModel sermonEditViewModel = new SermonEditViewModel(sermon, repository.GetLocations(), repository.GetPreachers(),
                repository.GetSerieses(), repository.GetSections());
            return View(sermonEditViewModel);
        }

        //
        // POST: /Sermon/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SermonEditViewModel sermonViewModel)
        {
            Sermon sermon = repository.GetSermonById(sermonViewModel.Id);

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
                sermon.SermonLocation = (sermonViewModel.LocationId != null) ? repository.GetLocationById(sermonViewModel.LocationId.Value) : null;

            if (((sermon.SermonPreacher != null) ? sermon.SermonPreacher.Id : 0) != sermonViewModel.PreacherId)
               sermon.SermonPreacher = (sermonViewModel.PreacherId != null) ? repository.GetPreacherById(sermonViewModel.PreacherId.Value) : null;

            if (((sermon.SermonSeries != null) ? sermon.SermonSeries.Id : 0) != sermonViewModel.SeriesId)
                sermon.SermonSeries = (sermonViewModel.SeriesId != null) ? repository.GetSeriesById(sermonViewModel.SeriesId.Value) : null;

            if (((sermon.SermonSection != null) ? sermon.SermonSection.Id : 0) != sermonViewModel.SectionId)
                sermon.SermonSection = (sermonViewModel.SectionId != null) ? repository.GetSectionById(sermonViewModel.SectionId.Value) : null;

                //TODO: SermonMedia = repository.GetMediaById(sermonViewModel.SermonMedia.Media

            if (ModelState.IsValid)
            {
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(sermon);
        }

        //
        // GET: /Sermon/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Sermon sermon = repository.GetSermonById(id);
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
            Sermon sermon = repository.GetSermonById(id);
            sermon.SermonMedia = null;
            repository.DeleteSermon(sermon.Id);
            repository.Save();
            return RedirectToAction("Index");
        }
    }
}