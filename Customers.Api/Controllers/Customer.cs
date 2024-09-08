using Customers.Application.UseCases.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer : ControllerBase
    {
        private readonly IMediator _mediator;

        public Customer(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateCustomer")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> CreateCustomer(CreateCustomerRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);

                return Ok(response);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
