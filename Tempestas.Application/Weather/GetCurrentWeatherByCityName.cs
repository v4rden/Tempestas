namespace Tempestas.Application.Weather
{
    using MediatR;

    public class GetCurrentWeatherByCityName : IRequest<CurrentWeatherInTownModel>
    {
        public string CityName { get; set; }
    }
}