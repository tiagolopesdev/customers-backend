using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        //public readonly IMediator _mediator;

        //public Products(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        [HttpGet("TestDate")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> TestDate()
        {
            try
            {
                var date = DateTime.Now;

                object dates = new
                {
                    brasil = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time")),
                    eua = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Australia Standard Time")),
                };

                return Ok(dates);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("HasStockProduct/{id}")]
        //[Produces("application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<Guid>> GetByNameProduct(Guid id)
        //{
        //    try
        //    {
        //        var response = await _mediator.Send(new HasStockProductRequest(id));

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("GetByNameProduct")]
        //[Produces("application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<Guid>> GetByNameProduct([FromQuery] string? name)
        //{
        //    try
        //    {
        //        var response = await _mediator.Send(new GetByNameProductRequest(name));

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("CreateProductByExcel")]
        //[Produces("multipart/form-data", "application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<Guid>> CreateProductByExcel(CreateProductsByExcelRequest request)
        //{
        //    try
        //    {
        //        var response = await _mediator.Send(request);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("CreateProduct")]
        //[Produces("application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<Guid>> CreateProduct(CreateProductRequest request)
        //{
        //    try
        //    {
        //        var response = await _mediator.Send(request);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("UpdateProduct")]
        //[Produces("application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<Guid>> UpdateProduct(UpdateProductRequest request)
        //{
        //    try
        //    {
        //        var response = await _mediator.Send(request);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
