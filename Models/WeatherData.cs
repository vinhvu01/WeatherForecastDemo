using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherForecast.Models
{
    public class WeatherData
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("days")]
        public List<Days> Days { get; set; }
    }

    public class Days
    {
        [JsonProperty("datetime")]
        public string DateTime { get; set; }

        [JsonProperty("windgust")]
        public long Windgust { get; set; }

        [JsonProperty("windspeed")]
        public long WindSpeed { get; set; }

        [JsonProperty("tempmin")]
        public double TempMin { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("tempmax")]
        public double TempMax { get; set; }

        [JsonProperty("feelslike")]
        public double FeelsLike { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("pressure")]
        public string Pressure { get; set; }

        [JsonProperty("humidity")]
        public string Humidity { get; set; }

        [JsonProperty("hours")]
        public List<Hours> Hours { get; set; }
    }

    public class Hours
    {
        [JsonProperty("datetime")]
        public string DateTime { get; set; }

        [JsonProperty("windgust")]
        public long Windgust { get; set; }

        [JsonProperty("windspeed")]
        public long WindSpeed { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feelslike")]
        public double FeelsLike { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("pressure")]
        public string Pressure { get; set; }

        [JsonProperty("humidity")]
        public string Humidity { get; set; }

        [JsonProperty("winddir")]
        public string WindDir { get; set; }
    }
}