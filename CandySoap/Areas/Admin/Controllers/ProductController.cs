using Microsoft.AspNetCore.Mvc;
using CandySoap.Models;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CandySoap.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Covertypes
        public IActionResult Index()

        {

            return View();
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


        // GET: Covertypes/Edit/5
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _db.Category.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                CoverTypeList = _db.Covers.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
            };

            if (id == null || _db.Covers == null)
            {//Create Product

                //ViewBag.covertypeList = covertypeslist;
                //ViewBag.categoryList = categoryList;

                return View(productVM);
            }
            else
            {
                //Update Product
                productVM.Product = _db.Product.GetFirstOrDefault(x => x.Id == id);
                return View(productVM);
            }

            
        }

        // POST: Covertypes/Upsert/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.Name);
                    if(obj.Product.ImageUrl!= null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\images\product\" + fileName + extension;
                   
                }
                if(obj.Product.Id == 0)
                {
                    _db.Product.Add(obj.Product);
                }
                else
                {
                    _db.Product.Update(obj.Product);
                }
                
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
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _db.Product.GetAll(includeProperties:"Category");
            var objlis = Json(new { data = productList });
            return (objlis);
        }
        #endregion

    }
}


