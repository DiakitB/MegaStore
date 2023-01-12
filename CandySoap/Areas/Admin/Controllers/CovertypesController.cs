using Microsoft.AspNetCore.Mvc;
using CandySoap.Models;
using CandySoap.DataAccess.Repository.IRepository;

namespace CandySoap.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CovertypesController : Controller
    {
        private readonly IUnitOfWork _db;

        public CovertypesController(IUnitOfWork db)
        {
            _db = db;
        }

        // GET: Covertypes
        public IActionResult Index()

        {
            IEnumerable<Covertype> categoryList = _db.Covers.GetAll();
            return View(categoryList);
        }

        // GET: Covertypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _db.Covers == null)
            {
                return NotFound();
            }
            var obj = _db.Covers.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // GET: Covertypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Covertypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Covertype obj)
        {
            if (ModelState.IsValid)
            {
                _db.Covers.Add(obj);
                _db.Save();
            }
            return RedirectToAction("Index");
        }

        // GET: Covertypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _db.Covers == null)
            {
                return NotFound();
            }
            var obj = _db.Covers.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: Covertypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Covertype obj)
        {
            if (ModelState.IsValid)
            {
                _db.Covers.Update(obj);
                _db.Save();
            }
            return RedirectToAction("Index");
        }

        // GET: Covertypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _db.Covers == null)
            {
                return NotFound();
            }
            var obj = _db.Covers.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: Covertypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == null || _db.Covers == null)
            {
                return NotFound();
            }
            var obj = _db.Covers.GetFirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                _db.Covers.Remove(obj);
                _db.Save();
            }
            return RedirectToAction("Index");
        }

    }
}
