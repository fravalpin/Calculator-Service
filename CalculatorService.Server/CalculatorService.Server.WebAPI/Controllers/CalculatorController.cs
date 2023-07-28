using CalculatorService.Server.Application.UsesCases;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    }
}
