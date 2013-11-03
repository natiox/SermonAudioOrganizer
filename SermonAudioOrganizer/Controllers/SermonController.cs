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
        public ActionResult Create(Sermon sermon)
        {
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
        public ActionResult Edit(Sermon sermon)
        {
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