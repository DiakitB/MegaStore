using Microsoft.AspNetCore.Mvc;
using CandySoap.Models;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using CandySoap.DataAccess.Repository;

namespace CandySoap.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CompanyController : Controller
    {
        private readonly IUnitOfWork _db;
       

        public CompanyController(IUnitOfWork db)
        {
            _db = db;
           
        }

        // GET: Covertypes
        public IActionResult Index()

        {

            return View();
        }

        // GET: Covertypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _db.Company == null)
            {
                return NotFound();
            }
            var obj = _db.Company.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // GET: Covertypes/Create


        // GET: Covertypes/Edit/5
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || _db.Company == null)
            {//Create Product

                //ViewBag.covertypeList = covertypeslist;
                //ViewBag.categoryList = categoryList;

                return View(company);
            }
            else
            {
                //Update Product
                company = _db.Company.GetFirstOrDefault(x => x.Id == id);
                return View(company);
            }


        }

        // POST: Covertypes/Upsert/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
               
                if (obj.Id == 0)
                {
                    _db.Company.Add(obj);
                }
                else
                {
                    _db.Company.Update(obj);
                }
                _db.Save();
               
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        // GET: Covertypes/Delete/5
      

        // POST: Covertypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == null || _db.Company == null)
            {
                return NotFound();
            }
            var obj = _db.Company.GetFirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                _db.Company.Remove(obj);
                _db.Save();
            }
            return RedirectToAction("Index");
        }
        #region API CALLS
        [HttpGet]
		public IActionResult GetAll()
		{
			var companyList = _db.Company.GetAll();
			return Json(new { data = companyList });
		}

		//POST
		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var obj = _db.Company.GetFirstOrDefault(u => u.Id == id);
			if (obj == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}

			_db.Company.Remove(obj);
			_db.Save();
			return Json(new { success = true, message = "Delete Successful" });

		}
		#endregion


	}

}
