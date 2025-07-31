using Omni.Domain;

namespace Compliance.Domain.Entity.SimSwapTask.Event;

public record SimSwapTaskExpiredDomainEvent(Guid TaskId, DateTime ExpiredOn, string Status)
    : DomainEventBase(nameof(SimSwapTaskExpiredDomainEvent));