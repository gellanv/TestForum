using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestSaganenko.Data;
using TestSaganenko.Models;

namespace TestSaganenko.Controllers
{
    public class TopicController : Controller
    {
        private readonly TestSaganenkoContext _context;

        public TopicController(TestSaganenkoContext context)
        {
            _context = context;
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var topic = await _context.Topics
                .Include(t => t._user)
                .Include(x => x.Posts)
                .ThenInclude(x => x._user)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create(int? id)
        {
            ViewBag.CatId = id;
            ViewBag.UserId = _context.Users.SingleOrDefault(x => x.UserName == User.Identity.Name).Id;
            return View();
        }

        // POST: Topics/Create      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,UserId,CategoryId")] Topic topic)
        {
            DateTime localDate = DateTime.Now;
            topic.DateTopic = localDate;
            if (ModelState.IsValid)
            {
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Category", new { @id = topic.CategoryId });
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", topic.UserId);
            return RedirectToAction("Details", "Category", new { @id = topic.CategoryId });
        }
    }
}
