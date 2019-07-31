namespace Tempestas.Application.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using Weather;

    public class TempestasWeatherProvider : IWeatherProvider
    {
        private readonly List<IWeatherClient> _clients;

        public TempestasWeatherProvider(IEnumerable<IWeatherClient> clients)
        {
            _clients = new List<IWeatherClient>();
            _clients.AddRange(clients);
        }

        public async Task<CurrentWeatherInTownModel> GetWeatherAsync(GetWeatherMsg msg,
            CancellationToken cancellationToken)
        {
            var work = new List<Task<CurrentWeatherInTownModel>>();

            foreach (var weatherClient in _clients) work.Add(weatherClient.GetWeatherAsync(msg, cancellationToken));

            var result = await Task.WhenAny(work).Result;
            return result;
        }
    }
}