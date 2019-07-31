namespace Tempestas.Infrastructure
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Weather;
    using Microsoft.Extensions.Options;

    public abstract class BaseWeatherClient : IWeatherClient
    {
        protected IOptions<ApiKeys> Options;

        protected BaseWeatherClient(IOptions<ApiKeys> options)
        {
            Options = options;
        }

        protected string ApiUrl { get; set; }

        protected abstract string ApiKey { get; }

        public Task<CurrentWeatherInTownModel> GetWeatherAsync(GetWeatherMsg msg, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var requestUrl = GetRequestUrl(msg.CityName);
                var response =
                    client.GetAsync(requestUrl,
                        cancellationToken).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                var result = Convert(data);
                return result;
            }
        }

        protected abstract string GetRequestUrl(string city);

        protected abstract Task<CurrentWeatherInTownModel> Convert(string json);

        protected CurrentWeatherInTownModel SignResult(CurrentWeatherInTownModel model)
        {
            model.Origin = ApiUrl;
            return model;
        }
    }
}