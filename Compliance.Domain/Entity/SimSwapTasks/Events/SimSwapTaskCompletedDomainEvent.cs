using Omni.Domain;

namespace Compliance.Domain.Entity.SimSwapTask.Event;

public record SimSwapTaskCompletedDomainEvent(Guid TaskId, DateTime CompletedOn, string Status)
    : DomainEventBase(nameof(SimSwapTaskCompletedDomainEvent));