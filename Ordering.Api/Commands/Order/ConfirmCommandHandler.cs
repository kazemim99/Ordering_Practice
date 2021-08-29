﻿using MediatR;
using Ordering.Domain.AggregatesModel;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Api.Commands.Order
{
    // Regular CommandHandler
    public class ConfirmCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public ConfirmCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when customer executes cancel order from app
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetConfirmStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}