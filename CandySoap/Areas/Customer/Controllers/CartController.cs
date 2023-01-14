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
				ShoppingCartVM.CartTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
		}
		public IActionResult Summary()
		{
			//var claimsIdentity = (ClaimsIdentity)User.Identity;
			//var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			//ShoppingCartVM = new ShoppingCartVM()
			//{
			//	ListCart = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product")
			//};
			//foreach (var cart in ShoppingCartVM.ListCart)
			//{
			//	cart.Price = GetPriceBasedonQuantity(cart.Count, cart.Product.Price, cart.Product.Price50);
			//	ShoppingCartVM.CartTotal += (cart.Price * cart.Count);
			//}
			//return View(ShoppingCartVM);
			return View();
		}


		public IActionResult Plus(int cartId)
		{

			var cart = _db.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
			_db.ShoppingCart.IncrementCount(cart, 1);
			_db.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Minus(int cartId) 
		{
			
			var cart = _db.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
			if(cart.Count <= 1)
			{
				_db.ShoppingCart.Remove(cart);
			}
			else
			{
				_db.ShoppingCart.DecrementCount(cart, 1);
			}
			
			_db.Save();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Remove(int cartId)
		{

			var cart = _db.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
			_db.ShoppingCart.Remove(cart);
			_db.Save();
			return RedirectToAction(nameof(Index));
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
