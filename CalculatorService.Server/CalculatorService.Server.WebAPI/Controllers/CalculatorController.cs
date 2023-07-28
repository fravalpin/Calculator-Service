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

        [HttpPost("Add")]
        [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<double>> Add([FromBody] AdditionRequest additionRequest)
        {
            double result = await _mediatior.Send(additionRequest);
            return Ok(result);
        }
    }
}
