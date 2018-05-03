using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPS.Models;
using PagedList;

namespace EPS.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public async Task<ActionResult> Index(int? page)
        {
            var posts = db.Posts.Include(p => p.Blog);
            ViewBag.Blogs = await db.Blogs.ToListAsync();
            return View(await posts.ToListAsync().ContinueWith(r => r.Result.ToPagedList(page ?? 1, 3)));
        }

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.Include(d => d.Blog).SingleOrDefaultAsync(g => g.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Blogs = await db.Blogs.ToListAsync();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment([Bind(Include = "Id,Name,EmailAddress,Content,DateCreated,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.DateCreated = DateTime.Now;
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return PartialView("_CommentLiPartial", await db.Comments.Where(r => r.PostId == comment.PostId).ToListAsync());
            }
            return null;
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
