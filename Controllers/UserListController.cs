using FeedForSites.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FeedForSites.Controllers
{
    [Authorize]
    public class UserListController : Controller
    {
        //
        // GET: /UserList/
        private readonly FeedForSitesDb _db = new FeedForSitesDb();

        public ActionResult Index()
        {
            List<UserProfile> users = _db.UserProfiles.ToList();

            return View(users);
        }

        public ActionResult Add(int? id)
        {
            UserProfile addeduser = _db.UserProfiles.Find(id);

            UserProfile loggedUser = _db.UserProfiles.Find(Membership.GetUser().ProviderUserKey);

            Friendship friendship = new Friendship()
            {
                FriendOneId = loggedUser.UserId,
                FriendTwoId = Convert.ToInt32(id),
                Approval = false,
                Date = DateTime.Now
            };

            _db.Friendships.Add(friendship);
            _db.SaveChanges();

            List<UserProfile> users = _db.UserProfiles.ToList();
            ViewBag.Message = "You have send a request to add " + addeduser.UserName + " as a friend.";
            return View("Index", users);
        }

    }
}
