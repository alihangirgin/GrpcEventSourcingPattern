﻿using EventSourcingPattern.CommandEventsApi.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingPattern.CommandEventsApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody]CreateOrderCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
