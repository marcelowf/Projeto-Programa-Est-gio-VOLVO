using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;

namespace TRABALHO_VOLVO
{
    public class GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GeneralExceptionHandler> logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            var (StatusCode, Title) = MapException(exception);
            using (StreamWriter writer = new StreamWriter(@"logs\exceptionsLog.txt", true))
            {
                await writer.WriteLineAsync(JsonConvert.SerializeObject(new
                {
                    DataHora = DateTime.Now.ToString("dd/MM/yy HH:mm:ss"),
                    statusCode = StatusCode,
                    machineName = Environment.MachineName,
                    TraceId = traceId,
                    ExceptionMessage = exception.Message,
                    StackInfo = exception
                }));
                writer.Flush();
            }
            // logger.LogError(
            //     exception,
            //     "Nao foi possivel processar request na maquina {MachineName}. Traceid: {traceID}",
            //     Environment.MachineName,
            //     traceId
            // );

            // await Results.Problem(
            //     type: null,
            //     title: Title,
            //     statusCode: StatusCode
            //     // extensions: new Dictionary<string, object?>
            //     // {
            //     //     {"traceId", traceId}
            //     // }
            // ).ExecuteAsync(httpContext);
            await httpContext.Response.WriteAsJsonAsync($"statusCode: {StatusCode}, Message: {Title}");

            return true;
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch
            {
                _ => (StatusCodes.Status501NotImplemented, "Ops! Parece que algo deu errado... Tente novamente.")
            };
        }

    }

}