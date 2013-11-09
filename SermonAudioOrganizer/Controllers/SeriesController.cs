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
    public class SeriesController : Controller
    {
        private SermonContext db = new SermonContext();

        //
        // GET: /Series/

        public ActionResult Index()
        {
            return View(db.Serieses.ToList());
        }

        //
        // GET: /Series/Details/5

        public ActionResult Details(int id = 0)
        {
            Series series = db.Serieses.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        //
        // GET: /Series/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Series/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Series series)
        {
            if (ModelState.IsValid)
            {
                db.Serieses.Add(series);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(series);
        }

        //
        // GET: /Series/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Series series = db.Serieses.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        //
        // POST: /Series/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Series series)
        {
            if (ModelState.IsValid)
            {
                db.Entry(series).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(series);
        }

        //
        // GET: /Series/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Series series = db.Serieses.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        //
        // POST: /Series/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Series series = db.Serieses.Find(id);
            db.Serieses.Remove(series);
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