using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace InternetShop.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment hostingenvironment;

        public BlogController(ApplicationDbContext? context, IWebHostEnvironment hc)
        {
            _context = context;
            hostingenvironment = hc;
        }

        // GET: /Blog
        public async Task<IActionResult> Index()
        {
            var blogPosts = await _context.BlogPosts.ToListAsync();
            return View(blogPosts);
        }

        // GET: /Blog/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(BlogPostVM blogPost1)
        {
            string filename = "";
            if (blogPost1.Photo != null)
            {
                string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "Image");
                filename = Guid.NewGuid().ToString() + "_" + blogPost1.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                blogPost1.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            BlogPost p = new BlogPost
            {
                Title = blogPost1.Title,
                Users = user,
                UserId = user.Id,
                Content = blogPost1.Content,
                Author = blogPost1.Author,
                image = filename
            };
            _context.BlogPosts.Add(p);
            _context.SaveChanges();
            ViewBag.success = "Record added";
            return RedirectToAction("Index", "Blog");
        }

        // GET: /Blog/Edit/5
        [Authorize] // Тільки адміністратор може редагувати публікації
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        // POST: /Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CreatedAt,Author,Image")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (blogPost.image == null)
            {
                try
                {
                    // Retrieve the existing entity from the database
                    var existingBlogPost = await _context.BlogPosts.FindAsync(id);
                    if (existingBlogPost == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the existing entity
                    existingBlogPost.Title = blogPost.Title;
                    existingBlogPost.Content = blogPost.Content;
                    existingBlogPost.CreatedAt = blogPost.CreatedAt;
                    existingBlogPost.Author = blogPost.Author;

                    // Save the changes
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        // GET: /Blog/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts.FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: /Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts.FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }


    }
}