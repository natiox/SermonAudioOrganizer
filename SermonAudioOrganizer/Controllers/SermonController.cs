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
            repository = new SermonRepository(new SermonContext());
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
                RecordingDate = sermonViewModel.RecordingDate,
                SectionIndex = sermonViewModel.SectionIndex,
                SeriesIndex = sermonViewModel.SeriesIndex,
                SeriesSubIndex = sermonViewModel.SeriesSubIndex,
                SermonLocation = repository.GetLocationById(sermonViewModel.LocationId),
                SermonPreacher = repository.GetPreacherById(sermonViewModel.PreacherId),
                SermonSeries = repository.GetSeriesById(sermonViewModel.SeriesId),
                SermonSection = repository.GetSectionById(sermonViewModel.SectionId),
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

            if (sermon.RecordingDate != sermonViewModel.RecordingDate)
                sermon.RecordingDate = sermonViewModel.RecordingDate;

            if (sermon.SectionIndex != sermonViewModel.SectionIndex)
                sermon.SectionIndex = sermonViewModel.SectionIndex;

            if (sermon.SeriesIndex != sermonViewModel.SeriesIndex)
                sermon.SeriesIndex = sermonViewModel.SeriesIndex;

            if (sermon.SeriesSubIndex != sermonViewModel.SeriesSubIndex)
                sermon.SeriesSubIndex = sermonViewModel.SeriesSubIndex;

            if (((sermon.SermonLocation != null) ? sermon.SermonLocation.Id : 0) != sermonViewModel.LocationId)
                sermon.SermonLocation = repository.GetLocationById(sermonViewModel.LocationId);

            if (((sermon.SermonPreacher != null) ? sermon.SermonPreacher.Id : 0) != sermonViewModel.PreacherId)
               sermon.SermonPreacher = repository.GetPreacherById(sermonViewModel.PreacherId);

            if (((sermon.SermonSeries != null) ? sermon.SermonSeries.Id : 0) != sermonViewModel.SeriesId)
                sermon.SermonSeries = repository.GetSeriesById(sermonViewModel.SeriesId);

            if (((sermon.SermonSection != null) ? sermon.SermonSection.Id : 0) != sermonViewModel.SectionId)
                sermon.SermonSection = repository.GetSectionById(sermonViewModel.SectionId);

                //TODO: SermonMedia = repository.GetMediaById(sermonViewModel.SermonMedia.Media

            if (ModelState.IsValid)
            {
                repository.UpdateSermon(sermon);
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
            repository.DeleteSermon(sermon.Id);
            repository.Save();
            return RedirectToAction("Index");
        }
    }
}