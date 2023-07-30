using CalculatorService.Server.Application.UsesCases;
using CalculatorService.Server.Application.UsesCases.QueryJournal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CalculatorService.Server.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IMediator _mediatior;

        public JournalController(IMediator mediatior)
        {
            _mediatior = mediatior;
        }

        [HttpPost("query")]
        [ProducesResponseType(typeof(QueryJournalResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<QueryJournalResponse>> Add([FromBody] QueryJournalRequest queryJournalRequest)
        {
            QueryJournalResponse result = await _mediatior.Send(queryJournalRequest);
            return Ok(result);
        }

    }
}
