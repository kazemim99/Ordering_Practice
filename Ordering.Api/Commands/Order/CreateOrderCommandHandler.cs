using MediatR;
using Ordering.Api.IntegrationEvents;
using Ordering.Api.IntegrationEvents.Events;
using Ordering.Domain.AggregatesModel;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Api.Commands.Order
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

        public async Task<bool> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
        {
            var orderStartedIntegrationEvent = new OrderCreatedIntegrationEvent(message.UserId);
            await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            var order = new Domain.AggregatesModel.Order.Order(message.UserId, message.UserName);

            foreach (var item in message.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.Units);
            }

            _orderRepository.Add(order);

            return await _orderRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }
}