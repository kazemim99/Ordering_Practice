using Ordering.Domain.Seed;
using System.Threading.Tasks;

namespace Ordering.Domain.AggregatesModel
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IOrderRepository : IRepository<Order.Order>
    {
        Order.Order Add(Order.Order order);

        void Update(Order.Order order);

        Task<Order.Order> GetAsync(int orderId);
    }
}