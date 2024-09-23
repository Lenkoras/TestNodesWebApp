using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using TestNodesWeb.Api.Data.Repositories;

namespace TestNodesWeb.Api.Configuration.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IJournalRepository journalRepository;
        private readonly ILogger<ApiExceptionFilterAttribute> logger;

        public ApiExceptionFilterAttribute(IJournalRepository journalRepository,
            ILogger<ApiExceptionFilterAttribute> logger)
        {
            this.journalRepository = journalRepository;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await HandleExceptionAsync(context)
                .ConfigureAwait(false);

            await base.OnExceptionAsync(context)
                .ConfigureAwait(false);
        }

        private async ValueTask HandleExceptionAsync(ExceptionContext context)
        {
            Type type = context.Exception.GetType();

            logger.LogInformation("Exception caught on {Path}, type - {Name}", context.HttpContext.Request.Path, type.Name);

            context.Result = await HandleExceptionAsyncCore(context)
                .ConfigureAwait(false);

            context.ExceptionHandled = true;
        }

        private async ValueTask<IActionResult> HandleExceptionAsyncCore(ExceptionContext context)
        {
            Exception exception = context.Exception;
            long eventId = 0;

            try
            {
                var now = DateTime.UtcNow;
                eventId = now.Ticks;

                logger.LogError(exception, "Handled exception");

                StringBuilder b = new();
                b.Append("Path = ")
                    .AppendLine(context.HttpContext.Request.Path)
                    .Append("Query parameters = ");

                context.HttpContext.Request.Query
                    .ToList()
                    .ForEach(x => b.Append(x.Key)
                        .Append(": ")
                        .Append(x.Value)
                        .Append(','));

                context.HttpContext.Request.Body.Position = 0;
                using var streamReader = new StreamReader(context.HttpContext.Request.Body);
                var bodyContent = await streamReader.ReadToEndAsync()
                    .ConfigureAwait(false);
                b.AppendLine()
                    .Append("Body parameters = ")
                    .AppendLine(bodyContent)
                    .Append(exception);

                await journalRepository.AddAsync(
                    new Journal()
                    {
                        EventId = eventId,
                        CreatedAt = now,
                        Text = b.ToString()
                    }, CancellationToken.None)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An exception occurred while processing the exception. Couldn't save the journal.");
            }

            return new ObjectResult(exception is SecureException
                ? new
                {
                    Type = "Secure",
                    Id = eventId,
                    Data = new { exception.Message }
                }
                : new
                {
                    Type = nameof(Exception),
                    Id = eventId,
                    Data = new { Message = $"Internal server error ID = {eventId}" }
                })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}