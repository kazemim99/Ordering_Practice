using EventBus.Events;
using System.Collections.Generic;

namespace Ordering.Api.IntegrationEvents.Events
{
    public record OrderStatusChangedToConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public string OrderStatus { get; }
        public string BuyerName { get; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToConfirmedIntegrationEvent(int orderId, string orderStatus, string buyerName,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }

    public record OrderStockItem
    {
        public int ProductId { get; }
        public int Units { get; }

        public OrderStockItem(int productId, int units)
        {
            ProductId = productId;
            Units = units;
        }
    }
}