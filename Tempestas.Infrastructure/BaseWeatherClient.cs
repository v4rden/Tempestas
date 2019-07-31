namespace Tempestas.Infrastructure
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Weather;

    public abstract class BaseWeatherClient : IWeatherClient
    {
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }

        public Task<CurrentWeatherInTownModel> GetWeatherAsync(GetWeatherMsg msg, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var requestUrl = $"{ApiUrl}{msg.CityName}{ApiKey}";
                    var response =
                        client.GetAsync(requestUrl,
                            cancellationToken).Result;
                    var data = response.Content.ReadAsStringAsync().Result;
                    return Convert(data);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        protected async virtual Task<CurrentWeatherInTownModel> Convert(string json)
        {
            throw new NotImplementedException();
        }
    }
}