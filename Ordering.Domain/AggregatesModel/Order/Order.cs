using Ordering.Domain.Events;
using Ordering.Domain.Exceptions;
using Ordering.Domain.Seed;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.AggregatesModel.Order
{
    public class Order : Entity, IAggregateRoot
    {
        private DateTime _orderDate;

        public int? GetBuyerId => _buyerId;

        private int? _buyerId;

        public OrderStatus OrderStatus { get; private set; }

        private int _orderStatusId;

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, int units)
        {
            var existingOrderForProduct = _orderItems
                .SingleOrDefault(o => o.ProductId == productId);
            if (existingOrderForProduct != null)
            {
                if (discount > existingOrderForProduct.GetCurrentDiscount())
                {
                    existingOrderForProduct.SetNewDiscount(discount);
                }
                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                var orderItem = new OrderItem(productId, productName, unitPrice, discount, units);
                _orderItems.Add(orderItem);
            }
        }

        public Order(string userId, string userName,
           int? buyerId = null)
        {
            _buyerId = buyerId;
            _orderStatusId = OrderStatus.Drafted.Id;
            _orderDate = DateTime.UtcNow;

            int now = DateTime.UtcNow.Hour;

            if (now is < 8 or > 19)
            {
                throw new OrderingDomainException("Order registration must be between 8 and 19 o'clock");
            }
            if (buyerId == null)
            {
                throw new OrderingDomainException("Order should contains buyer");
            }
            if (GetTotal() < 50000)
            {
                throw new OrderingDomainException("Price should be greater than 50000");
            }
            AddOrderStartedDomainEvent(userId, userName);
        }

        public void SetCancelledStatus()
        {
            StatusChangeException(OrderStatus.Cancelled);

            _orderStatusId = OrderStatus.Cancelled.Id;
            AddDomainEvent(new OrderCancelledDomainEvent(this));
        }

        public void SetConfirmStatus()
        {
            StatusChangeException(OrderStatus.Confirmed);

            _orderStatusId = OrderStatus.Confirmed.Id;
            AddDomainEvent(new OrderConfirmedDomainEvent(this));
        }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new OrderingDomainException($"Is not possible to change the order status from {OrderStatus.Name} to {orderStatusToChange.Name}.");
        }

        private void AddOrderStartedDomainEvent(string userId, string userName)
        {
            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName);

            this.AddDomainEvent(orderStartedDomainEvent);
        }

        public decimal GetTotal()
        {
            return _orderItems.Sum(o => o.GetUnits() * o.GetUnitPrice());
        }
    }
}