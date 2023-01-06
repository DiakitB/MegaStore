using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;

namespace CandySoap.DataAccess.Repository
{
    public class CategoryRepository : Repository<Covertypes>, ICategoryRepository
    {
        private readonly ApplicationContext _context;

        public CategoryRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

       
        public void Update(Covertypes category)
        {
            _context.categories.Update(category);
        }
    }
}
