using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedForSites.Models;
using System.Web.Security;
using System.Threading;

namespace F4S.Controllers
{
    [Authorize]
    public class PrivateMessageController : Controller
    {
        private readonly FeedForSitesDb _db = new FeedForSitesDb();

        //
        // GET: /PrivateMessage/
        [Authorize]
        public ActionResult Index()
        {
            var user = Membership.GetUser(true);
            
            if (user != null)
            {
                int userId = Convert.ToInt32(user.ProviderUserKey);
                var privatemessages = _db.PrivateMessages.Where(p => p.ReceiverUserId == userId)
                .Include(p => p.SenderUser)
                .Include(p => p.ReceiverUser)
                .OrderByDescending(p => p.Date)
                .Select(p => new PrivateMessageViewModel
                {
                    SenderUserId = p.SenderUserId,
                    SenderUser = p.SenderUser,
                    Text = p.Text,
                    Date = p.Date
                }).ToList().GroupBy(p => p.SenderUserId).Select(grp => grp.FirstOrDefault());

                /*
                var privatemessages = from p in _db.PrivateMessages
                          where p.ReceiverUserId == userId
                          group p by p.SenderUserId into g
                          select g.FirstOrDefault();
                }
            
                */
                return View(privatemessages);
            }
            return Redirect("/Account/Login");
        }

        public PartialViewResult LoadMessages(int id)
        {
            //Thread.Sleep(2000);
            int userId = Convert.ToInt32(Membership.GetUser().ProviderUserKey);
            var messages = _db.PrivateMessages.Where(p => (p.ReceiverUserId == userId || p.SenderUserId == userId) && (p.ReceiverUserId == id || p.SenderUserId == id)).OrderBy(p => p.Date).ToList();

            return PartialView("_PartialMessage", messages);
        }
        //
        // GET: /PrivateMessage/Details/5

        public ActionResult Details(int id = 0)
        {
            PrivateMessage privatemessage = _db.PrivateMessages.Find(id);
            if (privatemessage == null)
            {
                return HttpNotFound();
            }
            return View(privatemessage);
        }

        //
        // GET: /PrivateMessage/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PrivateMessage/Create
        //

        [HttpPost]
        public ActionResult Create(PrivateMessage privatemessage, string ToWho)
        {
            privatemessage.Date = DateTime.Now;
            privatemessage.IsRead = false;
            var user = Membership.GetUser(true);

            privatemessage.SenderUserId = _db.UserProfiles.Find(user.ProviderUserKey).UserId;
            
            if (!ToWho.Contains(","))
                privatemessage.ReceiverUserId = _db.UserProfiles.Where(n => n.UserName == ToWho).FirstOrDefault().UserId;
            else
            { 
                //Burada string'i parçalayıp her receiverUsername için Id alınacak.
            }

            string conversationId1 = privatemessage.SenderUserId.ToString() + "-" + privatemessage.ReceiverUserId.ToString();
            string conversationId2 = privatemessage.ReceiverUserId.ToString() + "-" + privatemessage.SenderUserId.ToString();
            // Check if conversationId exists.

            var conversation = _db.PrivateMessages.Where(p => p.ConversationId == conversationId1 || p.ConversationId == conversationId2).FirstOrDefault();
            string conversationId = String.Empty;

            if(conversation != null)
                conversationId = conversation.ConversationId;

            // If there is an initiated conversation this message belongs to that queue
            if (conversationId != null || conversationId != String.Empty)
                privatemessage.ConversationId = conversationId;
            else
                privatemessage.ConversationId = privatemessage.SenderUserId + "-" + privatemessage.ReceiverUserId;

            if (ModelState.IsValid)
            {
                _db.PrivateMessages.Add(privatemessage);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(privatemessage);
        }

        //
        // GET: /PrivateMessage/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PrivateMessage privatemessage = _db.PrivateMessages.Find(id);
            if (privatemessage == null)
            {
                return HttpNotFound();
            }
            return View(privatemessage);
        }

        //
        // POST: /PrivateMessage/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrivateMessage privatemessage = _db.PrivateMessages.Find(id);
            _db.PrivateMessages.Remove(privatemessage);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}