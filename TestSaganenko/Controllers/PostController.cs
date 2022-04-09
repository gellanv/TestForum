using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestSaganenko.Data;
using TestSaganenko.Models;

namespace TestSaganenko.Controllers
{
    public class PostController : Controller
    {
        private readonly TestSaganenkoContext _context;

        public PostController(TestSaganenkoContext context)
        {
            _context = context;
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create(int id)
        {
            var _user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == User.Identity.Name);
            ViewBag.UserId = _user.Id;
            ViewBag.TopicId = id;
            return View();
        }

        // POST: Posts/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TextPost,TopicId,UserId")] Post post)
        {
            DateTime localDate = DateTime.Now;
            post.DatePost = localDate;

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Topic", new { @id = post.TopicId });
            }
            return RedirectToAction("Details", "Topic", new { @id = post.TopicId });
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TextPost,DatePost,UserId,TopicId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Topic", new { @id = post.TopicId });
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p._topic)
                .Include(p => p._user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Topic", new { @id = post.TopicId });
        }
        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
