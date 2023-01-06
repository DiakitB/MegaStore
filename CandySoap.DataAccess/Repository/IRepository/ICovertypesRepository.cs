using CandySoap.Models;

namespace CandySoap.DataAccess.Repository.IRepository
{
    public interface ICovertypesRepository : IRepository<Covertype>
    {
        void Update(Covertype covertype);
    }
}
