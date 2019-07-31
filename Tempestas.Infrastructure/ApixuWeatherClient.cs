namespace Tempestas.Infrastructure
{
    using System.Threading.Tasks;
    using Application.Weather;
    using Newtonsoft.Json;

    public class ApixuWeatherClient : BaseWeatherClient
    {
        public ApixuWeatherClient()
        {
            ApiUrl = "http://api.apixu.com/v1/current.json?key=041be6d421854a20962234134193007&q=";
            ApiKey = "";
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