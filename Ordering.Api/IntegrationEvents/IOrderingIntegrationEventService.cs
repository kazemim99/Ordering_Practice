using EventBus.Events;
using System;
using System.Threading.Tasks;

namespace Ordering.Api.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);

        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}