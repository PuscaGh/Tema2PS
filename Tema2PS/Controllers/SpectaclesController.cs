using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tema2PS.Models;

namespace Tema2PS.Controllers
{
    public class SpectaclesController : Controller
    {
        private SpectacleContext db = new SpectacleContext();

        //
        // GET: /Spectacles/Index

        public ActionResult Index()
        {
            return View(db.Spectacle.ToList());
        }

        //
        // GET: /Spectacles/Details/5

        public ActionResult Details(int id = 0)
        {
            Spectacle spectacle = db.Spectacle.Find(id);
            if (spectacle == null)
            {
                return HttpNotFound();
            }
            return View(spectacle);
        }

        //
        // GET: /Spectacles/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Spectacles/Create

        [HttpPost]
        public ActionResult Create(Spectacle spectacle)
        {
            if (ModelState.IsValid)
            {
                db.Spectacle.Add(spectacle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spectacle);
        }

        //
        // GET: /Spectacles/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Spectacle spectacle = db.Spectacle.Find(id);
            if (spectacle == null)
            {
                return HttpNotFound();
            }
            return View(spectacle);
        }

        //
        // POST: /Spectacles/Edit/5

        [HttpPost]
        public ActionResult Edit(Spectacle spectacle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spectacle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spectacle);
        }

        //
        // GET: /Spectacles/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Spectacle spectacle = db.Spectacle.Find(id);
            if (spectacle == null)
            {
                return HttpNotFound();
            }
            return View(spectacle);
        }

        //
        // POST: /Spectacles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Spectacle spectacle = db.Spectacle.Find(id);
            db.Spectacle.Remove(spectacle);
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