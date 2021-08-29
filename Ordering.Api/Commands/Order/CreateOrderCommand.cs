using MediatR;
using Ordering.Api.Extensions;
using Ordering.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Api.Commands.Order
{
    public class CreateOrderCommand : IRequest<bool>
    {
        private readonly List<OrderItemDTO> _orderItems;

        public string UserId { get; private set; }

        public string UserName { get; private set; }

        public IEnumerable<OrderItemDTO> OrderItems => _orderItems;

        public CreateOrderCommand()
        {
            _orderItems = new List<OrderItemDTO>();
        }

        public CreateOrderCommand(List<BasketItem> basketItems, string userId, string userName) : this()
        {
            _orderItems = basketItems.ToOrderItemsDTO().ToList();
            UserId = userId;
            userName = UserName;
        }
    }

    public record OrderItemDTO
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal Discount { get; init; }
        public int Units { get; init; }
    }
}