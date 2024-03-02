using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (blogPost1.photo != null)
            {
                string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "Image");
                filename = Guid.NewGuid().ToString() + "_" + blogPost1.photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                blogPost1.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            BlogPost p = new BlogPost
            {
                Title = blogPost1.Title,
                Content = blogPost1.Content,
                Author = blogPost1.Author,
                image = filename
            };
            _context.BlogPosts.Add(p);
            _context.SaveChanges();
            ViewBag.success = "Record added";
            return View();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CreatedAt, Author")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
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
            return View(blogPost);
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