using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;

namespace CandySoap.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationContext _context;

        public ShoppingCartRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

       
        //public void Update(ShoppingCart shoppingcart)
        //{
        //    _context.shoppingCarts.Update(shoppingcart);
        //}
    }
}
