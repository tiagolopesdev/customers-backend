using Customers.Application.UseCases.ProductUseCases.CreateProduct;
using Customers.Application.UseCases.ProductUseCases.CreateProductsByExcel;
using Customers.Application.UseCases.ProductUseCases.GetByNameProduct;
using Customers.Application.UseCases.ProductUseCases.HasStockProduct;
using Customers.Application.UseCases.ProductUseCases.UpdateProduct;
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

        [HttpGet("HasStockProduct/{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> GetByNameProduct(Guid id)
        {
            try
            {
                var response = await _mediator.Send(new HasStockProductRequest(id));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByNameProduct")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> GetByNameProduct([FromQuery] string? name)
        {
            try
            {
                var response = await _mediator.Send(new GetByNameProductRequest(name));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateProductByExcel")]
        [Produces("multipart/form-data", "application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> CreateProductByExcel(CreateProductsByExcelRequest request)
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

        [HttpPost("CreateProduct")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> CreateProduct(CreateProductRequest request)
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
        
        [HttpPut("UpdateProduct")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> UpdateProduct(UpdateProductRequest request)
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
