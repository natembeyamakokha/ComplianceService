namespace Compliance.Shared.Domains
{
    using System;
    using Compliance.Shared.Domains.Events;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        #region entity members
        [Key]
        public TId Id { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } =  SystemClock.Now;
        public virtual bool IsDeleted { get; set; } = false;
        [MaxLength(200)]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public BaseEntity()
        {

        }
        public BaseEntity(TId id)
        {
            Id = id;
        }
        #endregion


        private List<IEvent> _events;
        public IReadOnlyCollection<IEvent> DomainEvents => _events;

        public void ClearDomainEvents()
            => _events?.Clear();


        protected void AddEvent(IEvent @event)
        {
            _events ??= new List<IEvent>();
            //take audit trail/snapshot

            _events.Add(@event);
        }


        public void Precondition(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        public void Precondition<T>(IBusinessRule<T> rule) where T : BaseEntity<Guid>
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException<T>(rule);
            }
        }
    }


}
