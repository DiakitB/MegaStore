using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;
using CandySoap.Models.ViewModels;
using CandySoap.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace CandySoap.Areas.Customer.Controllers
{
    [Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _db;
		[BindProperty]
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
				ListCart = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product"),
				OrderHeader = new()
			};
			foreach(var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedonQuantity(cart.Count, cart.Product.Price, cart.Product.Price50);
				ShoppingCartVM.OrderHeader.Ordertotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
		}
		public IActionResult Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM = new ShoppingCartVM()
			{
				ListCart = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product"),
				OrderHeader = new()
			};
			ShoppingCartVM.OrderHeader.ApplicationUser = _db.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
			ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
			foreach (var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedonQuantity(cart.Count, cart.Product.Price, cart.Product.Price50);
				ShoppingCartVM.OrderHeader.Ordertotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
			
		}
		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM.ListCart = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product");
			ShoppingCartVM.OrderHeader.PaymentStatus = DS.PaymentStatusPending;
			ShoppingCartVM.OrderHeader.OrderStatus= DS.StatusPending;
			ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;

			foreach (var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedonQuantity(cart.Count, cart.Product.Price, cart.Product.Price50);
				ShoppingCartVM.OrderHeader.Ordertotal += (cart.Price * cart.Count);
			}
			_db.OrderHeader.Add(ShoppingCartVM.OrderHeader);
			_db.Save();
			foreach (var cart in ShoppingCartVM.ListCart)
			{
				OrderDetail orderDetail = new()
				{
					ProductId = cart.ProductId,
					OrderId = ShoppingCartVM.OrderHeader.Id,
					Price = cart.Price,
					Count = cart.Count
				};
				_db.OrderDetail.Add(orderDetail);
				_db.Save();
			}
			//stripeSetting
			var domain = "https://localhost:7139/";
			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>()
				,
			Mode = "payment",
				SuccessUrl = domain+$"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
				CancelUrl = domain+"customer/cart/index",
			};
			foreach(var item in ShoppingCartVM.ListCart) 
			{

				var sessionLineItem = new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = (long)(item.Price * 100),
						Currency = "usd",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = item.Product.Name,
						},
					},
					Quantity = item.Count,
				};
				options.LineItems.Add(sessionLineItem);
		
			
			}

			var service = new SessionService();
			Session session = service.Create(options);
			_db.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
			_db.Save();
			Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);



			//_db.ShoppingCart.RemoveRange(ShoppingCartVM.ListCart);
			//_db.Save();
			//return RedirectToAction("Index", "Home");

		}
		public IActionResult OrderConfirmation(int id)
		{
			OrderHeader orderHeader = _db.OrderHeader.GetFirstOrDefault(u=>u.Id == id);
			var service = new SessionService();
			Session session = service.Get(orderHeader.SessionId);
			//check the stripe status
			if(session.PaymentStatus.ToLower() == "paid")
			{
				_db.OrderHeader.UpdateStatus(id, DS.StatusApproved, DS.PaymentStatusApproved);
				_db.Save();
			}
			List<ShoppingCart> shoppingCarts = _db.ShoppingCart.GetAll(u=>u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
			_db.ShoppingCart.RemoveRange(shoppingCarts);
			_db.Save();
			return View(id);

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
