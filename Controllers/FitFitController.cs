using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedForSites.Models;
using System.Web.Security;
using WebMatrix.WebData;
using FeedForSites.Filters;

namespace FeedForSites.Controllers
{
    public class FitFitController : Controller
    {
        private FeedForSitesDb db = new FeedForSitesDb();

        //
        // GET: /FitFit/

        public ActionResult Index()
        {
            var fitfits = db.FitFits.Include(f => f.User);
            return View(fitfits.ToList());
        }

        //
        // GET: /FitFit/Details/5

        public ActionResult Details(int id = 0)
        {
            FitFit fitfit = db.FitFits.Find(id);
            if (fitfit == null)
            {
                return HttpNotFound();
            }
            return View(fitfit);
        }

        //
        // GET: /FitFit/Create

        public ActionResult Create()
        {
            var user = db.UserProfiles.FirstOrDefault();
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /FitFit/Create

        [HttpPost]
        [InitializeSimpleMembership]
        public ActionResult Create(FitFit fitfit)
        {
            fitfit.Date = DateTime.Now;
            fitfit.UserId = WebSecurity.GetUserId(User.Identity.Name);

            if (ModelState.IsValid)
            {
                db.FitFits.Add(fitfit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", fitfit.UserId);
            return View(fitfit);
        }

        //
        // GET: /FitFit/Edit/5

        public ActionResult Edit(int id = 0)
        {
            FitFit fitfit = db.FitFits.Find(id);
            if (fitfit == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", fitfit.UserId);
            return View(fitfit);
        }

        //
        // POST: /FitFit/Edit/5

        [HttpPost]
        public ActionResult Edit(FitFit fitfit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fitfit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", fitfit.UserId);
            return View(fitfit);
        }

        //
        // GET: /FitFit/Delete/5

        public ActionResult Delete(int id = 0)
        {
            FitFit fitfit = db.FitFits.Find(id);
            if (fitfit == null)
            {
                return HttpNotFound();
            }
            return View(fitfit);
        }

        //
        // POST: /FitFit/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                FitFit fitfit = db.FitFits.Find(id);
                db.FitFits.Remove(fitfit);
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }
            catch
            {//TODO: Log error				
                return Content(Boolean.FalseString);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}