namespace Compliance.Shared.Domains.Events
{
    using System;
    using MediatR;
    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }

    public interface IDomainEventNotification<out TEventType>: IDomainEventNotification
    {
        TEventType DomainEvent { get; }
    }

}
