using FeedForSites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FeedForSites.Controllers
{
    public class HomeController : Controller
    {
        FeedForSitesDb _db = new FeedForSitesDb();

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var user = Membership.GetUser(true);
            var userId = _db.UserProfiles.Find(user.ProviderUserKey).UserId;
            var fitfits = _db.FitFits.Where(f => f.UserId == userId).OrderByDescending(f => f.Date);
            return View(fitfits);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Fitfit(string fit)
        { 
            var user = Membership.GetUser(true);

            FitFit fitfit = new FitFit{
                                          Date = DateTime.Now,
                                          Text = fit
                                      };

            fitfit.UserId = _db.UserProfiles.Find(user.ProviderUserKey).UserId;
            
            if (ModelState.IsValid)
            {
                _db.FitFits.Add(fitfit);
                _db.SaveChanges();

                //var fitfits = _db.FitFits.Where(f => f.UserId == fitfit.UserId).OrderByDescending(f => f.Date);
                return Content(fitfit);
            }

            return Content(Boolean.FalseString);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
