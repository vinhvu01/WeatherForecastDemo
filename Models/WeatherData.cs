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

        public string Period { get; set; }
    }

    public class Days
    {
        [JsonProperty("datetime")]
        public string DateTime { get; set; }

        [JsonProperty("windgust")]
        public string Windgust { get; set; }

        [JsonProperty("windspeed")]
        public long WindSpeed { get; set; }

        [JsonProperty("tempmin")]
        public string TempMin { get; set; }

        [JsonProperty("temp")]
        public string Temp { get; set; }

        [JsonProperty("tempmax")]
        public string TempMax { get; set; }

        [JsonProperty("feelslike")]
        public string FeelsLike { get; set; }

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
        public string Windgust { get; set; }

        [JsonProperty("windspeed")]
        public string WindSpeed { get; set; }

        [JsonProperty("temp")]
        public string Temp { get; set; }

        [JsonProperty("feelslike")]
        public string FeelsLike { get; set; }

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