using Omni.Domain;

namespace Compliance.Domain.Entity.SimSwapTask.Event;

public record SimSwapTaskFailedDomainEvent(Guid TaskId, DateTime failedOn, string Status)
    : DomainEventBase(nameof(SimSwapTaskFailedDomainEvent));