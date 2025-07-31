namespace Compliance.Shared.Domains.Events
{
    using System;

    public abstract class DomainEventBase : IEvent
    {
        public Guid Id { get; }

        public DateTime OccurredOn { get; }


        public DomainEventBase()
        {
            Id = Guid.NewGuid();
            OccurredOn = SystemClock.Now;
        }
    }



    public class DomainEventNotificationBase<T> : IDomainEventNotification<T> where T : IEvent
    {
        public T DomainEvent { get; }
        public Guid Id { get; }
        public DomainEventNotificationBase(T domainEvent, Guid id)
        {
            DomainEvent = domainEvent;
            Id = id;
        }
    }
}
