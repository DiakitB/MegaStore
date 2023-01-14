using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Security.Claims;
namespace CandySoap.Areas.Customer.Controllers
{
	[Area("Customer")]
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
		public IActionResult Details(int productId)

		{
            
            ShoppingCart cartObj = new()
            {
                Count = 1,
               ProductId= productId,
                Product = _db.Product.GetFirstOrDefault(x => x.Id == productId),
            };
			

			return View(cartObj);
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)

        {
            var claimsIdentity =(ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;
           _db.ShoppingCart.Add(shoppingCart);
            _db.Save();
            return RedirectToAction(nameof(Index));
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