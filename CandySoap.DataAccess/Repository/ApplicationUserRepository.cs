using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;

namespace CandySoap.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationContext _context;

        public ApplicationUserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

       
        //public void Update(Covertype covertypes)
        //{
        //    _context.covertypes.Update(covertypes);
        //}
    }
}
