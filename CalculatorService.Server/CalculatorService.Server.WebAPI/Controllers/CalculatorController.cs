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
        public async Task<ActionResult<AdditionResponse>> Add([FromBody] AdditionRequest additionRequest)
        {
            AdditionResponse result = await _mediatior.Send(additionRequest);
            return Ok(result);
        }

        [HttpPost("sub")]
        [ProducesResponseType(typeof(SubtractionResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SubtractionResponse>> Sub([FromBody] SubtractionRequest substractionRequest)
        {
            SubtractionResponse result = await _mediatior.Send(substractionRequest);
            return Ok(result);
        }

        [HttpPost("mult")]
        [ProducesResponseType(typeof(FactorResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FactorResponse>> Mult([FromBody] FactorRequest factorRequest)
        {
            FactorResponse result = await _mediatior.Send(factorRequest);
            return Ok(result);
        }

        [HttpPost("div")]
        [ProducesResponseType(typeof(DivisionResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DivisionResponse>> Div([FromBody] DivisionRequest divisionRequest)
        {
            DivisionResponse result = await _mediatior.Send(divisionRequest);
            return Ok(result);
        }
    }
}
