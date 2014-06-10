using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Soitin2;

namespace Soitin2.Controllers
{
    public class BrowserController : Controller
    {
        private MopidyContext db = new MopidyContext();

        // GET: /Browser/
        public ActionResult Index()
        {
            
            return View(db.TrackSet.ToList());
        }

        // GET: /Browser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackSet trackset = db.TrackSet.Find(id);
            if (trackset == null)
            {
                return HttpNotFound();
            }
            return View(trackset);
        }

        // GET: /Browser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Browser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,album,title,track,artist,genre,filename,runningtime,date,weight")] TrackSet trackset)
        {
            if (ModelState.IsValid)
            {
                db.TrackSet.Add(trackset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trackset);
        }

        // GET: /Browser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackSet trackset = db.TrackSet.Find(id);
            if (trackset == null)
            {
                return HttpNotFound();
            }
            return View(trackset);
        }

        // POST: /Browser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,album,title,track,artist,genre,filename,runningtime,date,weight")] TrackSet trackset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trackset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trackset);
        }

        // GET: /Browser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackSet trackset = db.TrackSet.Find(id);
            if (trackset == null)
            {
                return HttpNotFound();
            }
            return View(trackset);
        }

        // POST: /Browser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrackSet trackset = db.TrackSet.Find(id);
            db.TrackSet.Remove(trackset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
