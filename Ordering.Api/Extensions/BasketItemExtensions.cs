using Ordering.Api.Commands.Order;
using Ordering.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Api.Extensions
{
    public static class BasketItemExtensions
    {
        public static IEnumerable<OrderItemDTO> ToOrderItemsDTO(this IEnumerable<BasketItem> basketItems)
        {
            return basketItems.Select(ToOrderItemDto);
        }

        public static OrderItemDTO ToOrderItemDto(this BasketItem item)
        {
            return new OrderItemDTO()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity
            };
        }
    }
}