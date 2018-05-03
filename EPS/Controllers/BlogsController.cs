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
    public class BlogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blogs
        public async Task<ActionResult> Index()
        {
            return View(await db.Blogs.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<ActionResult> Details(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blogs = await db.Blogs.ToListAsync();
            ViewBag.Blogs = blogs;
            Blog blog = blogs.SingleOrDefault(r => r.Id == id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.Posts = blog.Posts.ToPagedList(page ?? 1, 3);
            return View(blog);
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
