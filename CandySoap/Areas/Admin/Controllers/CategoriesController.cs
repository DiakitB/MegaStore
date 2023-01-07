using Microsoft.AspNetCore.Mvc;
using CandySoap.Models;
using CandySoap.DataAccess.Repository.IRepository;

namespace CandySoap.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _context;

        public CategoriesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index()

        {
            IEnumerable<Category> categoryList = _context.Category.GetAll();
            return View(categoryList);
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }
            var obj = _context.Category.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _context.Category.Add(obj);
                _context.Save();
            }
            return RedirectToAction("Index");
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }
            var obj = _context.Category.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _context.Category.Update(obj);
                _context.Save();
            }
            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }
            var obj = _context.Category.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context == null)
            {
                return Problem("Entity set 'ApplicationContext.categories'  is null.");
            }
            var obj = _context.Category.GetFirstOrDefault(c => c.Id == id);
            if (obj != null)
            {
                _context.Category.Remove(obj);
            }

            _context.Save();
            return RedirectToAction("Index");
        }


    }
}
