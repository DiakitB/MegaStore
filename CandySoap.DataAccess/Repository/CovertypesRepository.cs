using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;

namespace CandySoap.DataAccess.Repository
{
    public class CovertypesRepository : Repository<Covertype>, ICovertypesRepository
    {
        private readonly ApplicationContext _context;

        public CovertypesRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

       
        public void Update(Covertype covertypes)
        {
            _context.covertypes.Update(covertypes);
        }
    }
}
