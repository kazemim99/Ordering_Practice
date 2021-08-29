using Ordering.Domain.AggregatesModel.Order;

namespace Ordering.UnitTests
{
    public class OrderBuilder
    {
        private readonly Order order;

        public OrderBuilder()
        {
            order = new Order(
                "userId",
                "fakeName"
             );
        }

        public OrderBuilder AddOne(
            int productId,
            string productName,
            decimal unitPrice,
            decimal discount,
            string pictureUrl,
            int units = 1)
        {
            order.AddOrderItem(productId, productName, unitPrice, discount, units);
            return this;
        }

        public Order Build()
        {
            return order;
        }
    }
}