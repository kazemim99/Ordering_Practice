using Ordering.Domain.Exceptions;
using Ordering.Domain.Seed;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.AggregatesModel
{
    public class OrderStatus
        : Enumeration
    {
        public static OrderStatus Confirmed = new OrderStatus(1, nameof(Confirmed).ToLowerInvariant());

        public static OrderStatus Drafted = new OrderStatus(3, nameof(Drafted).ToLowerInvariant());

        public static OrderStatus Cancelled = new OrderStatus(6, nameof(Cancelled).ToLowerInvariant());

        public OrderStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<OrderStatus> List() =>
            new[] { Confirmed, Drafted, Cancelled };

        public static OrderStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new OrderingDomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static OrderStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new OrderingDomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}