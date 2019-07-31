namespace Tempestas.Application.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using Weather;

    public interface IWeatherClient
    {
        Task<CurrentWeatherInTownModel> GetWeatherAsync(GetWeatherMsg msg, CancellationToken cancellationToken);
    }
}