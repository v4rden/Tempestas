namespace Tempestas.Infrastructure
{
    using System.Threading.Tasks;
    using Application.Weather;
    using Common;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class ApixuWeatherClient : BaseWeatherClient
    {
        public ApixuWeatherClient(IOptions<ApiKeys> options) : base(options)
        {
            ApiUrl = "http://api.apixu.com";
        }

        protected override string ApiKey => Options.Value.Apixu;

        protected override string GetRequestUrl(string city)
        {
            return $"{ApiUrl}/v1/current.json?{ApiKey}&q={city}";
        }

        protected override async Task<CurrentWeatherInTownModel> Convert(string json)
        {
            dynamic smth = JsonConvert.DeserializeObject(json);
            string townName = smth.location.name;
            decimal temperature = smth.current.temp_c;
            decimal pressure = smth.current.pressure_in;
            decimal humidity = smth.current.humidity;

            return new CurrentWeatherInTownModel
            {
                TownName = townName,
                Temperature = temperature,
                Pressure = pressure,
                Humidity = humidity
            };
        }
    }
}