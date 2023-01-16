using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;
using CandySoap.Models;

namespace CandySoap.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationContext _context;

        public OrderHeaderRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

       
        public void Update(OrderHeader order)
        {
            _context.orderHeaders.Update(order);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _context.orderHeaders.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if(paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
		}
        public void UpdateStripePaymentID(int id, string sessionId, string paymentItentId)
		{
			var orderFromDb = _context.orderHeaders.FirstOrDefault(x => x.Id == id);
            orderFromDb.SessionId = sessionId;
			orderFromDb.PaymentIntentId = paymentItentId;

		}
	}
}
