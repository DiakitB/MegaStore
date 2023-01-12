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

        }
        public ICategoryRepository Category { get; private set; }

        public ICovertypesRepository Covers { get; private set; }

        public IProductRepository Product { get; private set; }

		public ICompanyRepository Company { get; private set; }

		public void Save()
        {
            _db.SaveChanges();
        }
    }
}
