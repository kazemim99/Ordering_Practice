using EventBus.Events;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace IntegrationEventLogEF
{
    public class IntegrationEventLogEntry
    {
        public IntegrationEventLogEntry()
        {
        }

        public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
        {
        }

        public Guid EventId { get; private set; }

        public string TransactionId { get; private set; }

        public EventStateEnum State { get; set; }

        public DateTime CreationTime { get; private set; }

        [NotMapped]
        public IntegrationEvent IntegrationEvent { get; private set; }

        public string Content { get; private set; }

        public string EventTypeShortName { get; private set; }

        public int TimesSent { get; set; }

        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {
            IntegrationEvent = JsonSerializer.Deserialize(Content, type, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) as IntegrationEvent;
            return this;
        }
    }
}