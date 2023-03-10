using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;

namespace CandySoap.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _db;

        public UnitOfWork(ApplicationContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Covers = new CovertypesRepository(_db);
            Product= new ProductRepository(_db);
            Company= new CompanyRepository(_db);
            ApplicationUser= new ApplicationUserRepository(_db);
            ShoppingCart= new ShoppingCartRepository(_db);
            OrderDetail= new OrderDetailRepository(_db);
            OrderHeader= new OrderHeaderRepository(_db);

        }
        public ICategoryRepository Category { get; private set; }

        public ICovertypesRepository Covers { get; private set; }

        public IProductRepository Product { get; private set; }

		public ICompanyRepository Company { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

		public IOrderDetailRepository OrderDetail { get; private set; }

		public IOrderHeaderRepository OrderHeader { get; private set; }

		public void Save()
        {
            _db.SaveChanges();
        }
    }
}
