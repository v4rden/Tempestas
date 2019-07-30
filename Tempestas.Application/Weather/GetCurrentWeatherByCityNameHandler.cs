namespace Tempestas.Application.Weather
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class GetCurrentWeatherByCityNameHandler : IRequestHandler<GetCurrentWeatherByCityName, CurrentWeatherInTownModel>
    {
        public GetCurrentWeatherByCityNameHandler()
        {
            
        }

        public async Task<CurrentWeatherInTownModel> Handle(GetCurrentWeatherByCityName message,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}