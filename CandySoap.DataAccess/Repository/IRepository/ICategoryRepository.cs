using CandySoap.Models;

namespace CandySoap.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Covertypes>
    {
        void Update(Covertypes category);
    }
}
