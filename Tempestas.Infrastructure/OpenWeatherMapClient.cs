namespace Tempestas.Infrastructure
{
    using System.Threading.Tasks;
    using Application.Weather;
    using Newtonsoft.Json;

    public class OpenWeatherMapClient : BaseWeatherClient
    {
        public OpenWeatherMapClient()
        {
            ApiUrl = "http://api.openweathermap.org/data/2.5/weather?q=";
            ApiKey = "&appid=a98130182fe1762549fe72d3d4ca7d2a";
        }
        
        protected override async Task<CurrentWeatherInTownModel> Convert(string json)
        {
            dynamic smth = JsonConvert.DeserializeObject(json);
            string townName = smth.name;
            decimal temperature = smth.main.temp;
            decimal pressure = smth.main.pressure;
            decimal humidity = smth.main.humidity;
            
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