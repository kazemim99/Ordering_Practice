using MediatR;
using Ordering.Domain.AggregatesModel.Order;

namespace Ordering.Domain.Events
{
    public class OrderStartedDomainEvent : INotification
    {
        public string UserId { get; }

        public string UserName { get; }

        public Order Order { get; }

        public OrderStartedDomainEvent(Order order, string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            Order = order;
        }
    }
}