namespace Tempestas.Application.Weather
{
    public class CurrentWeatherInTownModel
    {
        public string TownName { get; set; }
        public decimal Temperature { get; set; }
        public decimal Pressure { get; set; }
        public decimal Humidity { get; set; }
    }
}