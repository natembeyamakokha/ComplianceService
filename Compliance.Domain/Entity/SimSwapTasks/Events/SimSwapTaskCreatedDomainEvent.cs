using Omni.Domain;

namespace Compliance.Domain.Entity.SimSwapTask.Event;

public record SimSwapTaskCreatedDomainEvent(Guid TaskId, DateTime CreatedOn, string Status)
    : DomainEventBase(nameof(SimSwapTaskCreatedDomainEvent));