using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using Customer.Application.Shared.Dtos;
using Customer.Application.UseCases.Create;
using Customer.Application.UseCases.GetByName;
using Customer.Application.UseCases.GetAll;
using Customer.Application.UseCases.Update;
using Customer.Application.UseCases.GetById;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet("TestDate")]
        //[Produces("application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<Guid>> TestDate()
        //{
        //    try
        //    {
        //        var date = DateTime.Now;

        //        object dates = new
        //        {
        //            brasil = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time")),
        //            eua = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Australia Standard Time")),
        //        };

        //        return Ok(dates);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut("UpdateCustomer")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> UpdateCustomer(UpdateCustomerCommand request)
        {
            try
            {
                var response = await _mediator.Send(request);
                //var response = await _customerModule.ExecuteCommandAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateCustomer")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> CreateCustomer(CreateCustomerCommand request)
        {
            try
            {
                var response = await _mediator.Send(request);
                //var response = await _customerModule.ExecuteCommandAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("ValidatePayment")]
        //[Produces("application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<Guid>> ValidatePayment(ValidatePaymentRequest request)
        //{
        //    try
        //    {
        //        var response = await _customerModule.ExecuteCommandAsync(request);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("CreateExcel")]
        //[Produces("application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<Guid>> CreateExcel()
        //{
        //    try
        //    {
        //        var response = await _customerModule.ExecuteCommandAsync(new CreateExcelRequest());

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("GetAllCustomer")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> GetAllCustomer([FromQuery] GetAllCustomerQuery request)
        {
            try
            { 
                var response = await _mediator.Send(request);
                //var response = await _customerModule.ExecuteQueryAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByNameCustomer")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<CustomerDto>>> GetByNameCustomer([FromQuery] GetByNameQuery request)
        {
            try
            {
                var response = await _mediator.Send(request);
                //var response = await _customerModule.ExecuteQueryAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByIdCustomer/{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Guid>> GetByNameCustomer(Guid id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString())) return BadRequest("Parametro id inválido");

                var response = await _mediator.Send(new GetByIdCustomerQuery(id));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
