using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public class RestService
    {
        HttpClient _client;
        

        public RestService()
        {
            _client = new HttpClient();

        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jsonSerializerOptions = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content, jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return weatherData;
        }
    }
}

