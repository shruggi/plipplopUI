using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Soitin2.Controllers
{
    public class PlaylistController : Controller
    {
        private MopidyContext db = new MopidyContext();
        //
        // GET: /Playlist/
        // coreplaylistdisplay
        public ActionResult Index()
        {
            var Quota = db.SettingsSet.Single().playlistquota;
            var UsedQuota = db.PlaylistSet.Where(a => a.owner.Equals(User.Identity.Name)).Count();
            var count = Quota - UsedQuota;

            ViewBag.Capacity = "You have " + count + " tracks left to add.";
            return View();
        }

        public ActionResult History()
        {
            var Quota = db.SettingsSet.Single().playlistquota;
            var UsedQuota = db.PlaylistSet.Where(a => a.owner.Equals(User.Identity.Name)).Count();
            var count = Quota - UsedQuota;

            ViewBag.Capacity = "You have " + count + " tracks left to add.";
            return View();
        }

        public ActionResult GetCoreTable(Models.jQueryDataTableParamModel param) {
            var result = new List<Models.BrowserTableModel>();
            result = db.PlaylistView.Select(a => new Models.BrowserTableModel() { id = a.Id, artist = a.artist, title = a.title, owner = a.owner}).ToList();
            IEnumerable<Models.BrowserTableModel> filteredresult;


            if (param.sSearch != null)
            {
                filteredresult = db.PlaylistView.Where(a => a.Id.ToString().Contains(param.sSearch) ||
                                                            a.owner.Contains(param.sSearch) ||
                                                            a.artist.Contains(param.sSearch) ||
                                                            a.title.Contains(param.sSearch)).Select(a => new Models.BrowserTableModel() { id = a.Id,  artist = a.artist, title = a.title, owner = a.owner }).ToList();
            }
            else
            {
                filteredresult = result;
            }

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Models.BrowserTableModel, string> orderingFunction = (a => sortColumnIndex == 0 ? a.id.ToString() :
                                                                            sortColumnIndex == 1 ? a.artist :
                                                                            sortColumnIndex == 2 ? a.title :
                                                                            a.owner);
            var sortDirection = Request["sSortDir_0"];

            if (sortDirection == "asc")
            {
                if (sortColumnIndex == 0)
                {
                    filteredresult = filteredresult.OrderBy(a => a.id);
                }
                else 
                {
                    filteredresult = filteredresult.OrderBy(orderingFunction);
                }
                
            }
            else
            {
                if (sortColumnIndex == 0)
                {
                    filteredresult = filteredresult.OrderByDescending(a => a.id);
                }
                else
                {
                    filteredresult = filteredresult.OrderByDescending(orderingFunction);
                }
            }

            var displayedresult = filteredresult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            List<string[]> resultarray = new List<string[]>();



            string controlcolumn = "";
            if (displayedresult != null)
            {
                foreach (var item in displayedresult)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var suppstatus = db.SupplementalPlaylistSet.Where(b => b.TrackId.Equals(item.id)).Select(b => b.Owner).ToList();
                        var corestatus = db.PlaylistView.Where(c => c.Id.Equals(item.id)).Select(c => c.owner).ToList();
                        if(suppstatus.Count() > 0)
                        {
                            if (suppstatus[0].ToString() == User.Identity.Name)
                            {
                                // Delete button
                                controlcolumn = "<button id=\""+ item.id + "\" class=\"btn btn-danger removeButton\">Remove</button>";
                            }
                            else
                            {
                                // Already added button
                                controlcolumn = "<button id=\""+ item.id + "\" class=\"btn btn-default alreadyButton\">Already Added</button>";
                            }
                        }
                        else
                        {
                            // Add button
                            controlcolumn = "<button id=\""+ item.id + "\"class=\"btn btn-primary addButton\">Add</button>";
                        }
                        if (corestatus.Count() > 0)
                        {
                            if (corestatus[0].ToString() == User.Identity.Name)
                            {
                                controlcolumn += "<button id=\"core" + item.id + "\" class=\"btn btn-danger removeCoreButton\">Remove from core</button>";
                            }
                        }
                    }
                    resultarray.Add(new string[] { item.id.ToString(), item.artist, item.title, item.owner, controlcolumn });
                }
            }


            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = result.Count(),
                iTotalDisplayRecords = filteredresult.Count(),
                aaData = resultarray
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQueuedTable(Models.jQueryDataTableParamModel param)
        {
            var result = db.QueueHistory.Select(a => new Models.QueueTableModel() { id = a.Id, addtime = a.addtime, artist = a.artist, title = a.title, source = a.source }).ToList();
            IEnumerable<Models.QueueTableModel> filteredresult;

            if (param.sSearch != null)
            {
                filteredresult = db.QueueHistory.Where(a => a.Id.ToString().Contains(param.sSearch) ||
                                                            a.addtime.ToString().Contains(param.sSearch) ||
                                                            a.artist.Contains(param.sSearch) ||
                                                            a.title.Contains(param.sSearch) ||
                                                            a.source.Contains(param.sSearch)).Select(a => new Models.QueueTableModel() { id = a.Id, addtime = a.addtime, artist = a.artist, title = a.title, source = a.source }).ToList();
            }
            else
            {
                filteredresult = result;
            }



            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Models.QueueTableModel, string> orderingFunction = (a => sortColumnIndex == 0 ? a.id.ToString() :
                                                                            sortColumnIndex == 1 ? a.addtime.ToString() :
                                                                            sortColumnIndex == 2 ? a.artist :
                                                                            sortColumnIndex == 3 ? a.title :
                                                                            a.source);

            var sortDirection = Request["sSortDir_0"];

            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 0:
                        filteredresult = filteredresult.OrderBy(a => a.id);
                        break;
                    case 1:
                        filteredresult = filteredresult.OrderBy(a => a.addtime);
                        break;
                    default:
                        filteredresult = filteredresult.OrderBy(orderingFunction);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 0:
                        filteredresult = filteredresult.OrderByDescending(a => a.id);
                        break;
                    case 1:
                        filteredresult = filteredresult.OrderByDescending(a => a.addtime);
                        break;
                    default:
                        filteredresult = filteredresult.OrderByDescending(orderingFunction);
                        break;
                }
            }

            var displayedresult = filteredresult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            List<string[]> resultarray = new List<string[]>();
            string controlcolumn = "";
            if (displayedresult != null)
            {
                foreach (var item in displayedresult)
                {
                    resultarray.Add(new string[] { item.id.ToString(), item.addtime.ToString(), item.artist, item.title, item.source, controlcolumn });
                }
            }
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = result.Count(),
                iTotalDisplayRecords = filteredresult.Count(),
                aaData = resultarray
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddCore(Models.jQueryTrackIdModel param)
        {
            bool returnstatus;
            int aa = 0;

            if (User.Identity.IsAuthenticated && param.iTrackId != 0)
            {
                // Does the track exist and doesn't already exist in userlist
                if (db.TrackSet.Where(a => a.Id.Equals(param.iTrackId)).Count() == 1)
                {
                    if (db.PlaylistSet.Where(a => a.TrackId.Equals(param.iTrackId)).Count() > 0)
                    {
                        aa = 1;
                        returnstatus = false;
                    }
                    else
                    {
                        PlaylistSet newtrack = new PlaylistSet();

                        newtrack.TrackId = param.iTrackId;
                        newtrack.owner = User.Identity.Name;

                        db.PlaylistSet.Add(newtrack);
                        db.SaveChanges();

                        returnstatus = true;
                    }
                }
                else
                {
                    returnstatus = false;
                }
            }
            else
            {
                returnstatus = false;
            }
            return Json(new { id = param.iTrackId, ok = returnstatus, alreadyadded = aa }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveCore(Models.jQueryTrackIdModel param)
        {
            bool returnstatus;

            if (User.Identity.IsAuthenticated && param.iTrackId != 0)
            {
                var result = db.PlaylistSet.Where(a => a.TrackId.Equals(param.iTrackId)).Single();

                if (result != null)
                {
                    db.PlaylistSet.Remove(result);
                    db.SaveChanges();
                    returnstatus = true;
                }
                else
                {
                    returnstatus = false;
                }
            }
            else
            {
                returnstatus = false;
            }
            return Json(new { id = param.iTrackId, ok = returnstatus }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(Models.jQueryTrackIdModel param)
        {
            bool returnstatus;

            if (User.Identity.IsAuthenticated && param.iTrackId != 0)
            {
                var result = db.SupplementalPlaylistSet.Where(a => a.TrackId.Equals(param.iTrackId)).Single();

                if (result != null)
                {
                    db.SupplementalPlaylistSet.Remove(result);
                    db.SaveChanges();
                    returnstatus = true;
                }
                else
                {
                    returnstatus = false;
                }
            }
            else
            {
                returnstatus = false;
            }
            return Json(new { id = param.iTrackId, ok = returnstatus }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(Models.jQueryTrackIdModel param)
        {
            bool returnstatus;
            int aa = 0;

            if (User.Identity.IsAuthenticated && param.iTrackId != 0)
            {
                // Does the track exist and doesn't already exist in userlist
                if (db.TrackSet.Where(a => a.Id.Equals(param.iTrackId)).Count() == 1)
                {
                    if (db.SupplementalPlaylistSet.Where(a => a.TrackId.Equals(param.iTrackId)).Count() > 0)
                    {
                        aa = 1;
                        returnstatus = false;
                    }
                    else
                    {
                        SupplementalPlaylistSet newtrack = new SupplementalPlaylistSet();

                        newtrack.TrackId = param.iTrackId;
                        newtrack.Owner = User.Identity.Name;

                        db.SupplementalPlaylistSet.Add(newtrack);
                        db.SaveChanges();

                        returnstatus = true;
                    }
                }
                else
                {
                    returnstatus = false;
                }
            }
            else
            {
                returnstatus = false;
            }
            return Json(new { id = param.iTrackId, ok = returnstatus, alreadyadded = aa }, JsonRequestBehavior.AllowGet);
        }
	}
}