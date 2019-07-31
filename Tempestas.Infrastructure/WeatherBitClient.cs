namespace Tempestas.Infrastructure
{
    using System.Threading.Tasks;
    using Application.Weather;
    using Common;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class WeatherBitClient : BaseWeatherClient
    {
        public WeatherBitClient(IOptions<ApiKeys> options) : base(options)
        {
            ApiUrl = "https://api.weatherbit.io";
        }

        protected override string ApiKey => Options.Value.WeatherBit;

        protected override string GetRequestUrl(string city)
        {
            return $"{ApiUrl}/v2.0/current?city={city}&{ApiKey}";
        }

        protected override async Task<CurrentWeatherInTownModel> Convert(string json)
        {
            dynamic smth = JsonConvert.DeserializeObject(json);
            string townName = smth.data[0].city_name;
            string pressure = smth.data[0].pres;
            string temperature = smth.data[0].temp;
            string humidity = smth.data[0].rh;

            return new CurrentWeatherInTownModel
            {
                TownName = townName,
                Temperature = decimal.Parse(temperature),
                Pressure = decimal.Parse(pressure),
                Humidity = decimal.Parse(humidity)
            };
        }
    }
}