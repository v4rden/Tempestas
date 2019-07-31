namespace Tempestas.Infrastructure
{
    using System.Threading.Tasks;
    using Application.Weather;
    using Newtonsoft.Json;

    public class WeatherBitClient : BaseWeatherClient
    {
        public WeatherBitClient()
        {
            ApiUrl = "https://api.weatherbit.io/v2.0/current?city=";
            ApiKey = "&key=f3b62333fc5b48b78c6f48b1e5d85d7a";
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