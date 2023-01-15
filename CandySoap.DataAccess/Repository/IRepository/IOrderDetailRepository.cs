using CandySoap.Models;

namespace CandySoap.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail detail);
    }
}
