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
using PagedList.Mvc;
using System.Text;
using RotativaHQ.MVC5;

namespace EPS.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<string> imageFormats = new List<string>() { ".jpg", ".png", ".gif", ".jpeg" };

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Blogs
        public async Task<ActionResult> Blogs()
        {
            return View(await db.Blogs.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<ActionResult> BlogDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = await db.Blogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult CreateBlog()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBlog([Bind(Include = "Id,Title,BloggerName")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Blogs.Add(blog);
                await db.SaveChangesAsync();
                return RedirectToAction("Blogs");
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<ActionResult> EditBlog(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = await db.Blogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBlog([Bind(Include = "Id,Title,BloggerName")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Blogs");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<ActionResult> DeleteBlog(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = await db.Blogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("DeleteBlog")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBlogConfirmed(int id)
        {
            Blog blog = await db.Blogs.FindAsync(id);
            db.Blogs.Remove(blog);
            await db.SaveChangesAsync();
            return RedirectToAction("Blogs");
        }

        // GET: Batches
        public async Task<ActionResult> Batches()
        {
            return View(await db.Batches.ToListAsync());
        }

        // GET: Batches/Details/5
        public async Task<ActionResult> BatchDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = await db.Batches.FindAsync(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // GET: Batches/Create
        public ActionResult CreateBatch()
        {
            return View();
        }

        // POST: Batches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBatch([Bind(Include = "Id,BatchName,BatchCode,Year,DateCreated")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                batch.DateCreated = DateTime.Now;
                db.Batches.Add(batch);
                await db.SaveChangesAsync();
                return RedirectToAction("Batches");
            }
            return View(batch);
        }

        // GET: Batches/Edit/5
        public async Task<ActionResult> EditBatch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = await db.Batches.FindAsync(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // POST: Batches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBatch([Bind(Include = "Id,BatchName,BatchCode,Year")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                batch.DateCreated = DateTime.Now;
                db.Entry(batch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Batches");
            }
            return View(batch);
        }

        // GET: Batches/Delete/5
        public async Task<ActionResult> DeleteBatch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = await db.Batches.FindAsync(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("DeleteBatch")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBatchConfirmed(int id)
        {
            Batch batch = await db.Batches.FindAsync(id);
            db.Batches.Remove(batch);
            await db.SaveChangesAsync();
            return RedirectToAction("Batches");
        }

        // GET: Posts
        public async Task<ActionResult> Posts(int? page)
        {
            var posts = db.Posts.Include(p => p.Blog);
            return View(await posts.ToListAsync().ContinueWith(r => r.Result.ToPagedList(page ?? 1, 10)));
        }

        // GET: Posts/Details/5
        public async Task<ActionResult> PostDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.Include(f => f.Blog).SingleOrDefaultAsync(r => r.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult CreatePost()
        {
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost([Bind(Include = "Id,Title,DateCreated,Thumbnail,Content,BlogId")] Post post, IEnumerable<HttpPostedFileBase> images)
        {
            if (images.ElementAt(0) != null)
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
                                ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title");
                                return View(post);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Unsupported File Format. Please .jpeg, .png, .gif are required");
                            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title");
                            return View(post);
                        }
                    }

                    byte[] imageBytes = GetImageAsByteArray(image);
                    if (loopCount == 1)
                    {
                        post.Thumbnail = imageBytes;
                        post.ThumbnailContentType = image.ContentType;
                    }
                    if (post.PostImages == null)
                    {
                        post.PostImages = new List<PostImage>();
                    }
                    post.PostImages.Add(new PostImage() { Content = imageBytes, ContentType = image.ContentType, Post = post });
                    loopCount++;
                }
            }
            if (ModelState.IsValid)
            {
                post.DateCreated = DateTime.Now;
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Posts");
            }

            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title", post.BlogId);
            return View(post);
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

        // GET: Posts/Edit/5
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.Include(f => f.PostImages).SingleOrDefaultAsync(v => v.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title", post.BlogId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost([Bind(Include = "Id,Title,DateCreated,Content,BlogId")] Post temp, IEnumerable<HttpPostedFileBase> images)
        {
            foreach (var image in images)
            {
                if (image != null)
                {
                    if (imageFormats.Contains(Path.GetExtension(image.FileName).ToLower()))
                    {
                        if (image.ContentLength > (1024 * 1024))
                        {
                            ModelState.AddModelError("", "Images must be less than 1MB in size.");
                            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title");
                            return View(temp);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unsupported File Format. Please .jpeg, .png, .gif are required");
                        ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title");
                        return View(temp);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                bool first = true;
                Post post = await db.Posts.Include(f => f.PostImages).SingleOrDefaultAsync(r => r.Id == temp.Id);
                post.Title = temp.Title; post.DateCreated = temp.DateCreated;
                post.Content = temp.Content; post.BlogId = temp.BlogId;

                if (images.ElementAtOrDefault(0) != null && images.Count() > 0)
                {
                    if (post.PostImages.Count > 0)
                    {
                        db.PostImages.RemoveRange(post.PostImages);
                    }
                    foreach (var image in images)
                    {
                        if (image != null)
                        {
                            var content = GetImageAsByteArray(image);
                            if (first)
                            {
                                post.Thumbnail = content;
                                post.ThumbnailContentType = image.ContentType;
                                post.PostImages = new List<PostImage>();
                            }
                            post.PostImages.Add(new PostImage() { Content = content, ContentType = image.ContentType, Post = post });
                            first = false;
                        }
                    }
                }

                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Posts");
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title", temp.BlogId);
            return View(temp);
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePostConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Posts");
        }

        // GET: Comments
        public async Task<ActionResult> Comments(int? page)
        {
            var comments = db.Comments.Include(c => c.Post);
            return View(await comments.ToListAsync().ContinueWith(r => r.Result.ToPagedList(page ?? 1, 10)));
        }

        // GET: Comments/Details/5
        public async Task<ActionResult> CommentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult CreateComment()
        {
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateComment([Bind(Include = "Id,Name,EmailAddress,Content,DateCreated,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.DateCreated = DateTime.Now;
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("Comments");
            }

            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<ActionResult> EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comment.PostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditComment([Bind(Include = "Id,Name,EmailAddress,Content,DateCreated,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Comments");
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<ActionResult> DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCommentConfirmed(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
            return RedirectToAction("Comments");
        }

        // GET: Messages
        public async Task<ActionResult> Messages(int? page)
        {
            return View(await db.Messages.ToListAsync().ContinueWith(r => r.Result.ToPagedList(page ?? 1, 10)));
        }

        // GET: Messages/Details/5
        public async Task<ActionResult> MessageDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult CreateMessage()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMessage([Bind(Include = "Id,EmailAddress,Name,PhoneNumber,Content,IsTreated,DateCreated")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                await db.SaveChangesAsync();
                return RedirectToAction("Messages");
            }

            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<ActionResult> EditMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMessage([Bind(Include = "Id,EmailAddress,Name,PhoneNumber,Content,IsTreated,DateCreated")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Messages");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<ActionResult> DeleteMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("DeleteMessage")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMessageConfirmed(int id)
        {
            Message message = await db.Messages.FindAsync(id);
            db.Messages.Remove(message);
            await db.SaveChangesAsync();
            return RedirectToAction("Messages");
        }

        #region Message Replies
        // GET: MessageReplies
        public async Task<ActionResult> Replies()
        {
            var messageReplies = db.MessageReplies.Include(m => m.Message);
            return View(await messageReplies.ToListAsync());
        }

        // GET: MessageReplies/Details/5
        public async Task<ActionResult> ReplyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageReply messageReply = await db.MessageReplies.FindAsync(id);
            if (messageReply == null)
            {
                return HttpNotFound();
            }
            return View(messageReply);
        }

        // GET: MessageReplies/Create
        public ActionResult CreateReply()
        {
            ViewBag.MessageId = new SelectList(db.Messages, "Id", "EmailAddress");
            return View();
        }

        // POST: MessageReplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateReply([Bind(Include = "Id,Content,DateCreated,RepliedBy,MessageId")] MessageReply messageReply)
        {
            if (!ModelState.IsValid)
            {
                messageReply.DateCreated = DateTime.Now;
                messageReply.RepliedBy = "admin";
                db.MessageReplies.Add(messageReply);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MessageId = new SelectList(db.Messages, "Id", "EmailAddress", messageReply.MessageId);
            return View(messageReply);
        }

        // GET: MessageReplies/Edit/5
        public async Task<ActionResult> EditReply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageReply messageReply = await db.MessageReplies.FindAsync(id);
            if (messageReply == null)
            {
                return HttpNotFound();
            }
            ViewBag.MessageId = new SelectList(db.Messages, "Id", "EmailAddress", messageReply.MessageId);
            return View(messageReply);
        }

        // POST: MessageReplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditReply([Bind(Include = "Id,Content,DateCreated,RepliedBy,MessageId")] MessageReply messageReply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messageReply).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MessageId = new SelectList(db.Messages, "Id", "EmailAddress", messageReply.MessageId);
            return View(messageReply);
        }

        // GET: MessageReplies/Delete/5
        public async Task<ActionResult> DeleteReply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageReply messageReply = await db.MessageReplies.FindAsync(id);
            if (messageReply == null)
            {
                return HttpNotFound();
            }
            return View(messageReply);
        }

        // POST: MessageReplies/Delete/5
        [HttpPost, ActionName("DeleteReply")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteReplyConfirmed(int id)
        {
            MessageReply messageReply = await db.MessageReplies.FindAsync(id);
            db.MessageReplies.Remove(messageReply);
            await db.SaveChangesAsync();
            return RedirectToAction("Replies");
        }

        #endregion

        #region Location
        // GET: Locations
        public async Task<ActionResult> Locations(bool? treated, int? page)
        {
            ViewBag.Treated = treated;
            var locations = await db.Locations.OrderByDescending(r => r.DateCreated).ToListAsync();
            if (treated != null)
            {
                return View(locations.Where(r => r.IsTreated == treated).ToPagedList(page ?? 1, 10));
            }
            else
            {
                return View(locations.ToPagedList(page ?? 1, 10));
            }
        }

        public async Task<ActionResult> LocationTreated(int? id, bool? treated, int? page)
        {
            if (id != null)
            {
                var location = await db.Locations.SingleOrDefaultAsync(r => r.Id == id);
                if (location != null)
                {
                    location.IsTreated = true;
                    db.Entry(location).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Locations", new { treated, page });
                }
                else
                {
                    return View();
                }
            }

            return View();
        }

        public async Task<ActionResult> GetLocations(string format, bool? treated)
        {
            List<Location> locations;
            if (treated != null)
            {
                locations = await db.Locations.Where(r => r.IsTreated == treated).ToListAsync();
            }
            else
            {
                locations = await db.Locations.ToListAsync();
            }

            return new ViewAsPdf(locations) { FileName = "locations.pdf" };
            //return new ViewAsPdf(locations) {  FileName = "locations.pdf"};
        }

        public ActionResult GetDocument(string format, bool? treated)
        {
            if (!string.IsNullOrEmpty(format) && format == "pdf")
            {
                return new Rotativa.ActionAsPdf("GetLocations", new { format, treated }) { FileName = "Locations.pdf" };
            }
            else
            {
                return RedirectToAction("Locations");
            }
        }

        // GET: Locations/Details/5
        public async Task<ActionResult> LocationDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult CreateLocation()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLocation([Bind(Include = "Id,Name,LocationAddress,PhoneNumber,Thumbnail,AdditionalInformation,IsApproved,IsTreated")] Location location, IEnumerable<HttpPostedFileBase> images)
        {
            if (images.ElementAt(0) == null)
            {
                ModelState.AddModelError("", "Please provide an Image");
                return View(location);
            }
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
            if (ModelState.IsValid)
            {
                location.DateCreated = DateTime.Now;
                db.Locations.Add(location);
                await db.SaveChangesAsync();
                return RedirectToAction("Locations");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<ActionResult> EditLocation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLocation([Bind(Include = "Id,Name,LocationAddress,PhoneNumber,Thumbnail,AdditionalInformation,IsApproved,IsTreated")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Locations");
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<ActionResult> DeleteLocation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("DeleteLocation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteLocationConfirmed(int id)
        {
            Location location = await db.Locations.FindAsync(id);
            db.Locations.Remove(location);
            await db.SaveChangesAsync();
            return RedirectToAction("Locations");
        }

        #endregion

        #region Courses
        // GET: Courses
        public async Task<ActionResult> Courses()
        {
            return View(await db.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<ActionResult> CourseDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult CreateCourse()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCourse([Bind(Include = "Id,CourseName,CourseCode,DateCreated")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.DateCreated = DateTime.Now;
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("Courses");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> EditCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCourse([Bind(Include = "Id,CourseName,CourseCode,DateCreated")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Courses");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<ActionResult> DeleteCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCourseConfirmed(int id)
        {
            Course course = await db.Courses.FindAsync(id);
            db.Courses.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction("Courses");
        }

        #endregion

        #region Corp Member Roles
        // GET: CorpMemberRoles
        public async Task<ActionResult> CorpMemberRoles()
        {
            return View(await db.CorperRoles.ToListAsync());
        }

        // GET: CorpMemberRoles/Details/5
        public async Task<ActionResult> CorpMemberRoleDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorpMemberRole corpMemberRole = await db.CorperRoles.FindAsync(id);
            if (corpMemberRole == null)
            {
                return HttpNotFound();
            }
            return View(corpMemberRole);
        }

        // GET: CorpMemberRoles/Create
        public ActionResult CreateCorpMemberRole()
        {
            return View();
        }

        // POST: CorpMemberRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCorpMemberRole([Bind(Include = "Id,Title,DateCreated")] CorpMemberRole corpMemberRole)
        {
            if (ModelState.IsValid)
            {
                corpMemberRole.DateCreated = DateTime.Now;
                db.CorperRoles.Add(corpMemberRole);
                await db.SaveChangesAsync();
                return RedirectToAction("CorpMemberRoles");
            }

            return View(corpMemberRole);
        }

        // GET: CorpMemberRoles/Edit/5
        public async Task<ActionResult> EditCorpMemberRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorpMemberRole corpMemberRole = await db.CorperRoles.FindAsync(id);
            if (corpMemberRole == null)
            {
                return HttpNotFound();
            }
            return View(corpMemberRole);
        }

        // POST: CorpMemberRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCorpMemberRole([Bind(Include = "Id,Title,DateCreated")] CorpMemberRole corpMemberRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corpMemberRole).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CorpMemberRoles");
            }
            return View(corpMemberRole);
        }

        // GET: CorpMemberRoles/Delete/5
        public async Task<ActionResult> DeleteCorpMemberRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorpMemberRole corpMemberRole = await db.CorperRoles.FindAsync(id);
            if (corpMemberRole == null)
            {
                return HttpNotFound();
            }
            return View(corpMemberRole);
        }

        // POST: CorpMemberRoles/Delete/5
        [HttpPost, ActionName("DeleteCorpMemberRole")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCorpMemberRoleConfirmed(int id)
        {
            CorpMemberRole corpMemberRole = await db.CorperRoles.FindAsync(id);
            db.CorperRoles.Remove(corpMemberRole);
            await db.SaveChangesAsync();
            return RedirectToAction("CorpMemberRoles");
        }

        #endregion

        #region Corp Members
        // GET: CorpMembers
        public async Task<ActionResult> CorpMembers(int? page)
        {
            var corpMembers = db.CorpMembers.Include(c => c.Batch).Include(c => c.CorpMemberRole).Include(c => c.Course);
            return View(await corpMembers.ToListAsync().ContinueWith(r => r.Result.ToPagedList(page ?? 1, 10)));
        }

        // GET: CorpMembers/Details/5
        public async Task<ActionResult> CorpMemberDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorpMember corpMember = await db.CorpMembers.FindAsync(id);
            if (corpMember == null)
            {
                return HttpNotFound();
            }
            return View(corpMember);
        }

        // GET: CorpMembers/Create
        public ActionResult CreateCorpMember()
        {
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "BatchName");
            ViewBag.CorpMemberRoleId = new SelectList(db.CorperRoles, "Id", "Title");
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName");
            return View();
        }

        // POST: CorpMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCorpMember([Bind(Include = "Id,StateCode,FullName,CourseId,BatchId,IsExco,CorpMemberRoleId,DateOfBirth,CreatedBy,DateCreated")] CorpMember corpMember, IEnumerable<HttpPostedFileBase> images)
        {
            if (images.ElementAt(0) == null)
            {
                ModelState.AddModelError("", "Please provide an Image");
                return View(corpMember);
            }
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
                            return View(corpMember);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unsupported File Format. Please .jpeg, .png, .gif are required");
                        return View(corpMember);
                    }
                }

                byte[] imageBytes = GetImageAsByteArray(image);
                if (loopCount == 1)
                {
                    corpMember.Thumbnail = imageBytes;
                    corpMember.ThumbnailContentType = image.ContentType;
                }
                if (corpMember.CorpMemberImages == null)
                {
                    corpMember.CorpMemberImages = new List<CorpMemberImage>();
                }
                corpMember.CorpMemberImages.Add(new CorpMemberImage() { Content = imageBytes, ContentType = image.ContentType, Corper = corpMember });
                loopCount++;
            }

            if (ModelState.IsValid)
            {
                corpMember.DateCreated = DateTime.Now;
                db.CorpMembers.Add(corpMember);
                await db.SaveChangesAsync();
                return RedirectToAction("CorpMembers");
            }

            ViewBag.BatchId = new SelectList(db.Batches, "Id", "BatchName", corpMember.BatchId);
            ViewBag.CorpMemberRoleId = new SelectList(db.CorperRoles, "Id", "Title", corpMember.CorpMemberRoleId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", corpMember.CourseId);
            return View(corpMember);
        }

        // GET: CorpMembers/Edit/5
        public async Task<ActionResult> EditCorpMember(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorpMember corpMember = await db.CorpMembers.FindAsync(id);
            if (corpMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "BatchName", corpMember.BatchId);
            ViewBag.CorpMemberRoleId = new SelectList(db.CorperRoles, "Id", "Title", corpMember.CorpMemberRoleId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", corpMember.CourseId);
            TempData["corpMember"] = corpMember;
            return View(corpMember);
        }

        // POST: CorpMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCorpMember([Bind(Include = "Id,StateCode,FullName,CourseId,BatchId,IsExco,CorpMemberRoleId,DateOfBirth,CreatedBy,DateCreated")] CorpMember corpMember, HttpPostedFileBase image)
        {
            if (image != null)
            {
                if (imageFormats.Contains(Path.GetExtension(image.FileName).ToLower()))
                {
                    if (image.ContentLength > (1024 * 1024))
                    {
                        ModelState.AddModelError("", "Images must be less than 1MB in size.");
                        ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title");
                        return View(corpMember);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Unsupported File Format. Please .jpeg, .png, .gif are required");
                    ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title");
                    return View(corpMember);
                }
            }

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    corpMember.Thumbnail = GetImageAsByteArray(image);
                    corpMember.ThumbnailContentType = image.ContentType;
                }
                else
                {
                    corpMember.Thumbnail = ((CorpMember)TempData["corpMember"]).Thumbnail;
                    corpMember.ThumbnailContentType = ((CorpMember)TempData["corpMember"]).ThumbnailContentType;
                    TempData["corpMember"] = null;
                }
                db.Entry(corpMember).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CorpMembers");
            }
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "BatchName", corpMember.BatchId);
            ViewBag.CorpMemberRoleId = new SelectList(db.CorperRoles, "Id", "Title", corpMember.CorpMemberRoleId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", corpMember.CourseId);
            return View(corpMember);
        }

        // GET: CorpMembers/Delete/5
        public async Task<ActionResult> DeleteCorpMember(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorpMember corpMember = await db.CorpMembers.FindAsync(id);
            if (corpMember == null)
            {
                return HttpNotFound();
            }
            return View(corpMember);
        }

        // POST: CorpMembers/Delete/5
        [HttpPost, ActionName("DeleteCorpMember")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCorpMemberConfirmed(int id)
        {
            CorpMember corpMember = await db.CorpMembers.FindAsync(id);
            db.CorpMembers.Remove(corpMember);
            await db.SaveChangesAsync();
            return RedirectToAction("CorpMembers");
        }
        #endregion
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