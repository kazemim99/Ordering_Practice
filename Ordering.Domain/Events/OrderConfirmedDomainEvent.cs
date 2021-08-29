using MediatR;
using Ordering.Domain.AggregatesModel.Order;

namespace Ordering.Domain.Events
{
    public class OrderConfirmedDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderConfirmedDomainEvent(Order order)
        {
            Order = order;
        }
    }
}