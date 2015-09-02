﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SermonAudioOrganizer.Domain;

namespace SermonAudioOrganizer.Controllers
{
    [RequireHttps]
    public class SectionController : Controller
    {
        private SermonContext db = new SermonContext();

        //
        // GET: /Section/

        public ActionResult Index()
        {
            return View(db.Sections.ToList());
        }
        
        //
        // GET: /Section/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Section/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Section section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(section);
        }

        //
        // GET: /Section/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        //
        // POST: /Section/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(section);
        }

        //
        // GET: /Section/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        //
        // POST: /Section/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = db.Sections.Find(id);
            db.Sections.Remove(section);
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