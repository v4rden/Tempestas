namespace Tempestas.Application.Interfaces
{
    using System.Threading.Tasks;
    using Weather;

    public interface IWeatherProvider
    {
        Task GetWeatherAsync(GetWeatherMsg msg);
    }
}