using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Commands.Order;
using System.Threading.Tasks;

namespace Ordering.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("draft")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrderDraftAsync([FromBody] CreateOrderCommand createOrderDraftCommand)
        {
            return await _mediator.Send(createOrderDraftCommand);
        }
    }
}