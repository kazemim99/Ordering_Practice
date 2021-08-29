using EventBus.Events;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transationId);

        Task MarkEventAsInProgressAsync(Guid logEventEventId);

        Task MarkEventAsPublishedAsync(Guid eventId);

        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);

        Task MarkEventAsFailedAsync(Guid eventId);
    }
}