namespace Compliance.Shared.Domains.Events
{
    using System;
    using MediatR;
    using Compliance.Shared.Messaging;

    public abstract class EventBase : IEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }

        public EventBase()
        {
            Id = Guid.NewGuid();
            OccurredOn = SystemClock.Now;
        }
    }

    public interface IEvent : INotification, IMessage
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
