using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Soitin2.Controllers
{
    public class HomeController : Controller
    {
        private MopidyContext db = new MopidyContext();
        public ActionResult Index()
        {
            var Quota = db.SettingsSet.Single().playlistquota;
            var UsedQuota = db.PlaylistSet.Where(a => a.owner.Equals(User.Identity.Name)).Count();
            var count = Quota - UsedQuota;

            ViewBag.Capacity = "You have " + count + " tracks left to add.";

            ViewData["CurrentPlaylist"] = db.CurrentPlaylistSet.ToList();
            ViewData["SupplementalPlaylist"] = db.SupplementalPlaylistView.ToList();

            return View();
        }
        public ActionResult _CurrentPlaylist()
        {
            return PartialView(db.CurrentPlaylistSet.ToList());
        }
        public ActionResult _UserPlaylist()
        {
            return PartialView(db.SupplementalPlaylistView.ToList());
        }

        public ActionResult _GetCoreCapacity()
        {
            return PartialView();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}