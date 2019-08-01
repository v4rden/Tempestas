namespace Tempestas.WebApi.FunctionalTests.Controllers.Weather
{
    using System.Net;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using Xunit;

    public class GetCurrentWeatherByCityName : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetCurrentWeatherByCityName(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public void GivenValidCityName_RespondsWithOkStatus()
        {
            var response = _client.GetAsync("/api/weather/GetCurrentWeatherByCityName/London").Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void GivenValidCityName_ReturnsCurrentWeatherInTownModel()
        {
            //Act
            var response = _client.GetAsync("/api/weather/GetCurrentWeatherByCityName/London").Result;

            //Assert
            var data = response.Content.ReadAsStringAsync().Result;
            dynamic smth = JsonConvert.DeserializeObject(data);
            string townName = smth.townName;
            decimal temperature = smth.temperature;
            decimal pressure = smth.pressure;
            decimal humidity = smth.humidity;

            Assert.Equal("London", townName);
            Assert.True(temperature > 0);
            Assert.True(pressure > 0);
            Assert.True(humidity > 0);
        }

        [Fact]
        public void GivenValidCityName_ReturnsSignedModel()
        {
            //Act
            var response = _client.GetAsync("/api/weather/GetCurrentWeatherByCityName/London").Result;

            //Assert
            var data = response.Content.ReadAsStringAsync().Result;
            dynamic smth = JsonConvert.DeserializeObject(data);
            string origin = smth.origin;

            Assert.True(!string.IsNullOrEmpty(origin));
        }

        [Fact]
        public void GivenValidCityName_ReturnsModelWithElapsedTime()
        {
            //Act
            var response = _client.GetAsync("/api/weather/GetCurrentWeatherByCityName/london").Result;

            //Assert
            var data = response.Content.ReadAsStringAsync().Result;
            dynamic smth = JsonConvert.DeserializeObject(data);
            decimal elapsedTime = smth.elapsedTime;

            Assert.True(elapsedTime > 0);
        }
    }
}