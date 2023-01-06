using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;

namespace CandySoap.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

       
        public void Update(Product product)
        {
            _context.products.Update(product);
        }
    }
}
