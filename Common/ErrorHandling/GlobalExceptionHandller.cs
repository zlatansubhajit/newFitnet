using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using newFitnet.Common.BusinessRulesEngine;

namespace newFitnet.Common.ErrorHandling
{
    internal sealed class GlobalExceptionHandller(ILogger<GlobalExceptionHandller> logger) : IExceptionHandler
    {
        private const string? ServerError = "Server Error";
        private const string ErrorOccurredMessage = "An Error Occurred.";

        private static readonly Action<ILogger, string, Exception> LogException =
            LoggerMessage.Define<string>(LogLevel.Error, eventId:
                new EventId(0, "Error"), formatString: "{Message}");
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            LogException(logger,ErrorOccurredMessage, exception);
			ProblemDetails problemdetails;

			switch (exception)
			{
				case BusinessRuleValidationException businessRuleValidationException:
					problemdetails = new ProblemDetails
					{
						Status = StatusCodes.Status409Conflict,
						Title = businessRuleValidationException.Message
					};
					break;

				default:
					problemdetails = new ProblemDetails
					{
						Status = StatusCodes.Status500InternalServerError,
						Title = ServerError
					};
					break;
			}

			httpContext.Response.StatusCode = problemdetails.Status!.Value;
			await httpContext.Response.WriteAsJsonAsync(problemdetails, cancellationToken);

			return true;
		}
    }
}
