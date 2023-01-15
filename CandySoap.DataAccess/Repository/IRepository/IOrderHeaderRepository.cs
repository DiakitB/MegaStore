using CandySoap.Models;

namespace CandySoap.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader order);
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
    }
}
