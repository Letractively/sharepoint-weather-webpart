using Sumit.Webpart.Weather.Common;

namespace Sumit.Webpart.Weather.Entity
{
    public class WeatherProfile
    {
        public string CityName { get; set; }
        public bool isCondition { get; set; }
        public bool isConditionImage { get; set; }
        public bool isHighTemperature { get; set; }
        public bool isLowTemprature { get; set; }
        public Enums.TempUnitType UnitTemperature { get; set; }
        public bool isHumidity { get; set; }
        public bool isWind { get; set; }
        public bool isUpdateInfo { get; set; }
        public string Location { get; set; }
        public string HighTemperature { get; set; }
        public string Temperature { get; set; }
        public string Condition { get; set; }
        public string Day { get; set; }
        public string Date { get; set; }
        public string LowTemperature { get; set; }
        public string Humidity { get; set; }
        public string Wind { get; set; }
        public string ImagePath { get; set; }
        public string PublishedDate { get; set; }
    }
}
