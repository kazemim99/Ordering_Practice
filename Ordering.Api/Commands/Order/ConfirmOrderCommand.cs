using MediatR;
using System.Runtime.Serialization;

namespace Ordering.Api.Commands.Order
{
    public class ConfirmOrderCommand : IRequest<bool>
    {
        [DataMember]
        public int OrderNumber { get; set; }

        public ConfirmOrderCommand()
        {
        }

        public ConfirmOrderCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}