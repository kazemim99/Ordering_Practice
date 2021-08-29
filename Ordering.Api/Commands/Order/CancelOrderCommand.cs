using MediatR;
using System.Runtime.Serialization;

namespace Ordering.Api.Commands.Order
{
    public class CancelOrderCommand : IRequest<bool>
    {
        [DataMember]
        public int OrderNumber { get; set; }

        public CancelOrderCommand()
        {
        }

        public CancelOrderCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}