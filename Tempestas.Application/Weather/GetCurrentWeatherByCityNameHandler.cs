namespace Tempestas.Application.Weather
{
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using MediatR;

    public class GetCurrentWeatherByCityNameHandler :
        IRequestHandler<GetCurrentWeatherByCityName, CurrentWeatherInTownModel>
    {
        private readonly IWeatherProvider _weatherProvider;

        public GetCurrentWeatherByCityNameHandler(IWeatherProvider weatherProvider)
        {
            _weatherProvider = weatherProvider;
        }

        public async Task<CurrentWeatherInTownModel> Handle(GetCurrentWeatherByCityName request,
            CancellationToken cancellationToken)
        {
            var response = await _weatherProvider
                .GetWeatherAsync(new GetWeatherMsg {CityName = request.CityName}, cancellationToken);

            if (response == null)
            {
                //todo handler unsuccessful results;
            }

            return response;
        }
    }
}