using MediatR;
using System;
using System.Collections.Generic;

namespace Ordering.Domain.Seed
{
    public abstract class Entity
    {
        private int? _requestHashCode;

        public virtual int Id { get; }

        private List<INotification> _domainEvent;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvent?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvent ??= new List<INotification>();
            _domainEvent.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification notification)
        {
            _domainEvent?.Remove(notification);
        }

        public void ClearDomainEvents()
        {
            _domainEvent?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;
            if (object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();
            _requestHashCode ??= this.Id.GetHashCode();

            return _requestHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}