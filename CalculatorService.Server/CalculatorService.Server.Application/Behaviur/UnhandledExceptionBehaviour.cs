﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.Behaviour
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _log;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _log = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"Exception for request {request}");
                throw;
            }
        }
    }
}
