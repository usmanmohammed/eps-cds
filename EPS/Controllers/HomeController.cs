using EPS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace EPS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<string> imageFormats = new List<string>() { ".jpg", ".png", ".gif", ".jpeg" };

        public async Task<ActionResult> Index()
        {
            ViewBag.Posts = await db.Posts.Include(f => f.Blog).OrderBy(r => r.DateCreated).Take(3).ToListAsync();
            ViewBag.Birthdays = await db.CorpMembers.Where(r => r.DateOfBirth.Month.Equals(DateTime.Now.Month)).ToListAsync();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public async Task<ActionResult> Excos()
        {
            var excos = db.CorpMembers.Include(f => f.Batch).Include(f => f.Course).Include(f => f.CorpMemberRole).Where(f => f.IsExco);
            return View(await excos.ToListAsync());
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact([Bind(Include = "Id,EmailAddress,Name,PhoneNumber,Content,IsTreated,DateCreated")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.DateCreated = DateTime.Now;
                db.Messages.Add(message);
                await db.SaveChangesAsync();
                return RedirectToAction("SubmitConfirmation");
            }

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Id,Name,LocationAddress,PhoneNumber,Thumbnail,AdditionalInformation,IsApproved,IsTreated")] Location location, IEnumerable<HttpPostedFileBase> images)
        {
            int loopCount = 1;
            foreach (var image in images)
            {
                if (image != null)
                {
                    if (imageFormats.Contains(Path.GetExtension(image.FileName).ToLower()))
                    {
                        if (image.ContentLength > (1024 * 1024))
                        {
                            ModelState.AddModelError("", "Images must be less than 1MB in size.");
                            return View(location);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unsupported File Format. Please .jpeg, .png, .gif are required");
                        return View(location);
                    }

                    byte[] imageBytes = GetImageAsByteArray(image);
                    if (loopCount == 1)
                    {
                        location.Thumbnail = imageBytes;
                        location.ThumbnailContentType = image.ContentType;
                    }
                    if (location.LocationImages == null)
                    {
                        location.LocationImages = new List<LocationImage>();
                    }
                    location.LocationImages.Add(new LocationImage() { Content = imageBytes, ContentType = image.ContentType, Location = location });
                    loopCount++;
                }
            }
            if (ModelState.IsValid)
            {
                location.DateCreated = DateTime.Now;
                db.Locations.Add(location);
                await db.SaveChangesAsync();
                return RedirectToAction("SubmitConfirmation");
            }

            return View(location);
        }
        private byte[] GetImageAsByteArray(HttpPostedFileBase image)
        {
            string imageFileName = Path.GetFileName(image.FileName);
            byte[] imageByteArray = new byte[image.ContentLength];
            using (BinaryReader reader = new BinaryReader(image.InputStream))
            {
                imageByteArray = reader.ReadBytes(image.ContentLength);
            }
            return imageByteArray;
        }

        public ActionResult SubmitConfirmation()
        {
            return View();
        }

        public async Task<ActionResult> CorpMembers()
        {
            var corpMembers = await db.CorpMembers.Include(f => f.Batch).Include(f => f.Course)
                .Include(f => f.CorpMemberRole).Where(r => r.CorpMemberRole.Title != "CDS Manager").ToListAsync();
            return View(corpMembers.GroupBy(r => r.Batch.BatchCode));
        }

        public async Task<ActionResult> Search(string query, int? page)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var searchResults = await db.Posts.Include(r => r.Blog).Include(r => r.Comments).Where(r => r.Title.Contains(query) ||
            r.Content.Contains(query) || r.Blog.Title.Contains(query) || r.Comments.Select(f => f.Content).Contains(query)).ToListAsync()
            .ContinueWith(r => r.Result.ToPagedList(page ?? 1, 3));

            ViewBag.query = query;
            ViewBag.Blogs = await db.Blogs.ToListAsync();
            return View(searchResults);

        }
    }
}