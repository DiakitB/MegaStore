using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CandySoap.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()

        {
            IEnumerable<Product> productList =_db.Product.GetAll();
            
            return View(productList);
        }
		public IActionResult Details(int? id)

		{
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _db.Product.GetFirstOrDefault(x => x.Id == id),
            };
			

			return View(cartObj);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}