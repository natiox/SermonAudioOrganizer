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
    public class PreacherController : Controller
    {
        private SermonContext db = new SermonContext();

        //
        // GET: /Preacher/

        public ActionResult Index()
        {
            return View(db.Preachers.ToList());
        }

        //
        // GET: /Preacher/Details/5

        public ActionResult Details(int id = 0)
        {
            Preacher preacher = db.Preachers.Find(id);
            if (preacher == null)
            {
                return HttpNotFound();
            }
            return View(preacher);
        }

        //
        // GET: /Preacher/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Preacher/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Preacher preacher)
        {
            if (ModelState.IsValid)
            {
                db.Preachers.Add(preacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(preacher);
        }

        //
        // GET: /Preacher/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Preacher preacher = db.Preachers.Find(id);
            if (preacher == null)
            {
                return HttpNotFound();
            }
            return View(preacher);
        }

        //
        // POST: /Preacher/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Preacher preacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preacher);
        }

        //
        // GET: /Preacher/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Preacher preacher = db.Preachers.Find(id);
            if (preacher == null)
            {
                return HttpNotFound();
            }
            return View(preacher);
        }

        //
        // POST: /Preacher/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Preacher preacher = db.Preachers.Find(id);
            db.Preachers.Remove(preacher);
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