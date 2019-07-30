namespace Tempestas.Infrastructure
{
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Weather;

    public class OpenWeatherMapProvider : IWeatherProvider
    {
        public Task GetWeatherAsync(GetWeatherMsg msg)
        {
            throw new System.NotImplementedException();
        }
    }
}