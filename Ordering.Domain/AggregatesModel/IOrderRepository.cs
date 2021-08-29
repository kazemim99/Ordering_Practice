using Ordering.Domain.Seed;
using System.Threading.Tasks;

namespace Ordering.Domain.AggregatesModel
{
    public interface IOrderRepository : IRepository<Order.Order>
    {
        Order.Order Add(Order.Order order);

        void Update(Order.Order order);

        Task<Order.Order> GetAsync(int orderId);
    }
}