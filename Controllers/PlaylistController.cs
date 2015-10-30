using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myimportantproject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using myimportantproject.ViewModel;

namespace myimportantproject.Controllers
{
    public class PlaylistController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Playlist
        public ActionResult Index(int? id)
        {
            VideoPlayList videoPlayList = new VideoPlayList();
            ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            videoPlayList.Playlists = db.Playlists.Where(c=>c.ApplicationUserID==user.Id).Include(p => p.ApplicationUser).ToList();
            if (id != null)
            {
                ViewBag.PlayListID = id.Value;
                videoPlayList.Videos = videoPlayList.Playlists.Where(
                    i => i.PlaylistID == id.Value).Single().Videos.ToList();

            }
            return View(videoPlayList);
        }

        // GET: Playlist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        public ActionResult AddVideoToPlaylist(int id, int playlistID)
        {
            //if(id==null||playlistID==null)
            //return View();
            //else
            //{
            //    var video = db.Videos.Find(id);
            //    db.Playlists.Find(playlistID).Videos.Add(video);
            //    return View();
            //}
            var video = db.Videos.Find(id);
            db.Playlists.Find(playlistID).Videos.Add(video);
            db.SaveChanges();
            return View();
        }


        // GET: Playlist/Create
        public ActionResult Create()
        {
            return View(); 
        }

        // POST: Playlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaylistID,Title")] Playlist playlist)
        {
            
            if (ModelState.IsValid)
            {
                playlist.ApplicationUserID = User.Identity.GetUserId();
                db.Playlists.Add(playlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        // GET: Playlist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", playlist.ApplicationUserID);
            return View(playlist);
        }

        // POST: Playlist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaylistID,Title,ApplicationUserID")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", playlist.ApplicationUserID);
            return View(playlist);
        }

        // GET: Playlist/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            db.Playlists.Remove(playlist);
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
