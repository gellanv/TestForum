using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestSaganenko.Data;

namespace TestSaganenko.Controllers
{
    public class CategoryController : Controller
    {
        private readonly TestSaganenkoContext _context;

        public CategoryController(TestSaganenkoContext context)
        {
            _context = context;
        }
        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.Include(t => t.Topics).ThenInclude(t => t._user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}
