using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SermonAudioOrganizer.Domain;

namespace SermonAudioOrganizer.Controllers
{
    public class MediaController : Controller
    {
        private SermonContext db = new SermonContext();

        //
        // GET: /Media/

        public ActionResult Index()
        {
            return View(db.Medias.ToList());
        }
        
        //
        // GET: /Media/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Media/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Media media)
        {
            if (ModelState.IsValid)
            {
                db.Medias.Add(media);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(media);
        }

        //
        // GET: /Media/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Media media = db.Medias.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        //
        // POST: /Media/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Media media)
        {
            if (ModelState.IsValid)
            {
                db.Entry(media).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(media);
        }

        //
        // GET: /Media/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Media media = db.Medias.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        //
        // POST: /Media/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Media media = db.Medias.Find(id);
            db.Medias.Remove(media);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}