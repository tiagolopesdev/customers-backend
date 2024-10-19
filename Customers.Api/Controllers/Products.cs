using Customers.Application.UseCases.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        public readonly IMediator _mediator;

        public Products(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateProduct")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> CreateCustomer(CreateProductRequest request)
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
