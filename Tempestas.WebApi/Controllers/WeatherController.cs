namespace Tempestas.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Application.Weather;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class WeatherController : BaseController
    {
        [HttpGet("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CurrentWeatherInTownModel>> GetCurrentWeatherByCityName(string city)
        {
            return Ok(await Mediator.Send(new GetCurrentWeatherByCityName {CityName = city}));
        }
    }
}