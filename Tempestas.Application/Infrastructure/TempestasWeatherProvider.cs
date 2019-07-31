namespace Tempestas.Application.Infrastructure
{
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using Serilog;
    using Weather;

    public class TempestasWeatherProvider : IWeatherProvider
    {
        private readonly List<IWeatherClient> _clients;
        private readonly Stopwatch _timer;

        public TempestasWeatherProvider(IEnumerable<IWeatherClient> clients)
        {
            _clients = new List<IWeatherClient>();
            _clients.AddRange(clients);
            _timer = new Stopwatch();
        }

        public async Task<CurrentWeatherInTownModel> GetWeatherAsync(GetWeatherMsg msg,
            CancellationToken cancellationToken)
        {
            var work = new List<Task<CurrentWeatherInTownModel>>();

            foreach (var weatherClient in _clients) work.Add(weatherClient.GetWeatherAsync(msg, cancellationToken));

            _timer.Start();
            var result = await Task.WhenAny(work).Result;
            _timer.Stop();

            result.ElapsedTime = _timer.ElapsedMilliseconds;
            Log.Information($"Response from {result.Origin} was acquired in {_timer.ElapsedMilliseconds}ms");

            return result;
        }
    }
}