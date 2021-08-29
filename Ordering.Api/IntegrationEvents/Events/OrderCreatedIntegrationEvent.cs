using EventBus.Events;

namespace Ordering.Api.IntegrationEvents.Events
{
    public record OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; init; }

        public OrderCreatedIntegrationEvent(string userId)
            => UserId = userId;
    }
}