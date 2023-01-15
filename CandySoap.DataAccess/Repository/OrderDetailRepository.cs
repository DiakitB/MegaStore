using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;

namespace CandySoap.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationContext _context;

        public OrderDetailRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

       
        public void Update(OrderDetail detail)
        {
            _context.orderDetail.Update(detail);
        }
    }
}
