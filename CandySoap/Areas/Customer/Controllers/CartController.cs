using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CandySoap.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _db;
		public ShoppingCartVM ShoppingCartVM { get; set; }
		public int Ordertotal { get; set; }
		public CartController(IUnitOfWork db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM = new ShoppingCartVM()
			{
				ListCart = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product")
			};
			foreach(var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedonQuantity(cart.Count, cart.Product.Price, cart.Product.Price50);
			}
			return View(ShoppingCartVM);
		}
		private double GetPriceBasedonQuantity(double qunatity, double price, double price50)
		{
			if (qunatity <= 50)
			{
				return price;
			}
			else
			{
				return price50;
			}
		}

	}
}