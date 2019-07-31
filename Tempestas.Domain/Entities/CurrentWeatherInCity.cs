namespace Tempestas.Domain.Entities
{
    public class CurrentWeatherInCity
    {
        public string CityName { get; set; }
        public decimal Temperature { get; set; }
        public decimal Pressure { get; set; }
        public decimal Humidity { get; set; }
    }
}