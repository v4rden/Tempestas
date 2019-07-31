namespace Tempestas.Application.Infrastructure
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Serilog;

    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;

        public RequestPerformanceBehaviour()
        {
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var name = typeof(TRequest).Name;

            Log.Information($"Request: {name} processed in({_timer.ElapsedMilliseconds} milliseconds)");

            if (_timer.ElapsedMilliseconds > 500)
            {
                Log.Warning(
                    $"Request: {name} took too long({_timer.ElapsedMilliseconds} milliseconds) Request body: {@request}");
            }

            return response;
        }
    }
}