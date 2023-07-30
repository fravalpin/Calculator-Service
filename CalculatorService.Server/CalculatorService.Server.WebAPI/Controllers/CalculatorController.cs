using CalculatorService.Server.Application.UsesCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CalculatorService.Server.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IMediator _mediatior;

        public CalculatorController(IMediator mediatior)
        {
            _mediatior = mediatior;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(AdditionResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AdditionResponse>> Add([FromBody] AdditionBodyRequest additionRequest, [FromHeader/*(Name = "X‐Evi‐Tracking‐Id")*/] string? XEviTrackingId)
        {
            AdditionRequest request = new(additionRequest.Addends, XEviTrackingId);
            AdditionResponse result = await _mediatior.Send(request);
            return Ok(result);
        }

        [HttpPost("sub")]
        [ProducesResponseType(typeof(SubtractionResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SubtractionResponse>> Sub([FromBody] SubtractionBodyRequest substractionRequest, [FromHeader/*(Name = "X‐Evi‐Tracking‐Id")*/] string? XEviTrackingId)
        {
            SubtractionRequest request = new(substractionRequest.Minuend, substractionRequest.Subtrahend, XEviTrackingId);
            SubtractionResponse result = await _mediatior.Send(request);
            return Ok(result);
        }

        [HttpPost("mult")]
        [ProducesResponseType(typeof(FactorResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FactorResponse>> Mult([FromBody] FactorBodyRequest factorRequest, [FromHeader/*(Name = "X‐Evi‐Tracking‐Id")*/] string? XEviTrackingId)
        {
            FactorRequest request = new(factorRequest.Factors, XEviTrackingId);
            FactorResponse result = await _mediatior.Send(request);
            return Ok(result);
        }

        [HttpPost("div")]
        [ProducesResponseType(typeof(DivisionResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DivisionResponse>> Div([FromBody] DivisionBodyRequest divisionRequest, [FromHeader/*(Name = "X‐Evi‐Tracking‐Id")*/] string? XEviTrackingId)
        {
            DivisionRequest request = new(divisionRequest.Dividend, divisionRequest.Divisor, XEviTrackingId);
            DivisionResponse result = await _mediatior.Send(request);
            return Ok(result);
        }

        [HttpPost("sqrt")]
        [ProducesResponseType(typeof(SquareRootResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SquareRootResponse>> Sqrt([FromBody] SquareRootBodyRequest squareRootRequest, [FromHeader/*(Name = "X‐Evi‐Tracking‐Id")*/] string? XEviTrackingId)
        {
            SquareRootRequest request = new(squareRootRequest.Number, XEviTrackingId);
            SquareRootResponse result = await _mediatior.Send(request);
            return Ok(result);
        }
    }
}
