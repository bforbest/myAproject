using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myimportantproject.Models;
using webpagetest.Models;
using webpagetest.Models.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace myimportantproject.Controllers
{
    public class VideoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        VideoRepository repository = new VideoRepository();
        // GET: Video
        public async Task<ActionResult> Index(int? SelectedCategory)
        {
            var departments = db.Categories.OrderBy(q => q.Title).ToList();
            ViewBag.SelectedCategory = new SelectList(departments, "CategoryID", "Title", SelectedCategory);
            int categoryID = SelectedCategory.GetValueOrDefault();

            IQueryable<Video> videos = db.Videos
                .Where(c => !SelectedCategory.HasValue || c.CategoryID == categoryID)
                .OrderBy(d => d.CategoryID)
                .Include(d => d.Category);
            var sql = videos.ToString();
            return View(await videos.ToListAsync());
        }

        // GET: Video/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await db.Videos.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }
        [ChildActionOnly]
        public  PartialViewResult PartialCarousel(int? id)
        {
            Video video =  db.Videos.Find(id);
            IQueryable<Video> videos = db.Videos.Where(c => c.CategoryID == video.CategoryID);            
            return PartialView("_Carousel", videos);
        }




        // GET: Video/Create
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
            return View();
        }

        // POST: Video/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VideoID,Name,IsEmbed,VideoUrl,Discription,ImageUrl,thumbsUp,thumbsDown,CategoryID")] Video video)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Videos.Add(video);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            PopulateCategoryDropDownList(video.CategoryID);
            return View(video);
        }

        // GET: Video/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await db.Videos.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            PopulateCategoryDropDownList(video.CategoryID);
            return View(video);
        }

        // POST: Video/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VideoID,Name,IsEmbed,VideoUrl,Discription,ImageUrl,thumbsUp,thumbsDown, CategoryID")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: Video/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await db.Videos.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Video/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Video video = await db.Videos.FindAsync(id);
            db.Videos.Remove(video);
            await db.SaveChangesAsync();
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


        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var categorysQuery = from d in db.Categories
                                   orderby d.Title
                                   select d;
            ViewBag.CategoryID = new SelectList(categorysQuery, "CategoryID", "Title", selectedCategory);
        }

        //[HttpPost]
        [HttpPost]
        public int thumbsUp(string id)
        {
            repository.thumbsUp(id);
            repository.SaveChanges();
            int count = repository.thumbsUpCount(id);
            return (count);
        }
        [HttpPost]
        public int thumbsDown(string id)
        {
            repository.thumbsDown(id);
            repository.SaveChanges();
            int count = repository.thumbsDownCount(id);
            return (count);
        }
        //add a video to favorite of a user. return 1 if add 0 if remove
        //to do return 2 if over the limit of playlist capacity
        public int AddToFav(int? id, bool? isOn)
        {
            if (id != null)
            {
                int i =AddToFavOrWatch((int)id, "Favorite", isOn);
                return i;
            }
            return 9;
        }
        public int AddToWatchLater(int? id, bool? isOn)
        {
            if (id != null)
            {
                int i = AddToFavOrWatch((int)id, "WatchLater", isOn);
                return i;
            }
            return 9;
        }
        //Method to find a video of specific user and put it to favorite or watchlater list
        //nullable bool to check if the video is already on favorite list of the user to switch on the favorite button
        private int AddToFavOrWatch(int id, string fav, bool? isOnlist=false)
        {
            ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());

                var video = db.Videos.Find(id);
                Playlist playlist = db.Playlists.Where(c => c.ApplicationUserID == user.Id).
                    Where(c => c.Title == fav).Single();
            if (!playlist.Videos.Contains(video))
            {
                //if only to check the video is on playlist
                if (isOnlist == true) { return 3; }
                playlist.Videos.Add(video);
                db.SaveChanges();
                return 1;
            }
            else
            {
                if (isOnlist == true) { return 2; }
                playlist.Videos.Remove(video);
                db.SaveChanges();
                return 0;
            }
                

        }


    }
}
