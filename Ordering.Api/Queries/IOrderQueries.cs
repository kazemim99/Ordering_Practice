using System.Threading.Tasks;

namespace Ordering.Api.Queries
{
    public interface IOrderQueries
    {
        Task<Order> GetOrderAsync(int id);
    }
}