using CalculatorService.Server.Application.Abstractions;
using CalculatorService.Server.Domain.Calculations;
using CalculatorService.Server.Domain.Journal;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases.QueryJournal
{
    public record QueryJournalRequest(string Id) : IRequest<QueryJournalResponse>;
    public record QueryJournalResponse(IEnumerable<OperationJournal>? Operations);
    public class QueryJournalRequestHandler : IRequestHandler<QueryJournalRequest, QueryJournalResponse>
    {
        private readonly ILogger<QueryJournalRequestHandler> _logger;
        private readonly IJournalService _journalService;

        public QueryJournalRequestHandler(ILogger<QueryJournalRequestHandler> logger, IJournalService journalService)
        {
            _logger = logger;
            _journalService = journalService;
        }

        public Task<QueryJournalResponse> Handle(QueryJournalRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Asking Journal for {request.Id}");
            if (request?.Id == null) throw new ArgumentNullException();

            List<OperationJournal>? operations = _journalService.Get(request.Id) as List<OperationJournal>;
            _logger.LogDebug($"Journal for {request.Id} has {(operations != null? operations.Count : 0)} operations");

            return Task.FromResult(new QueryJournalResponse(operations));
        }
    }

    public class QueryJournalValidator : AbstractValidator<QueryJournalRequest>
    {
        public QueryJournalValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
        }
    }

}
