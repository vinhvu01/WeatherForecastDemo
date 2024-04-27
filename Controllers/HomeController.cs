using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Models;
using WeatherForecast.Services;
using static System.Net.WebRequestMethods;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static readonly string ApiKey = "FD6XX363KUVNCP74P5E489F8G";

        private static readonly string UrlTimeLine = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline";

        public RestService _rs { get; set; }
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _rs = new RestService();
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index Request");
            return View();
        }
        

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult WeatherData(string id)
        {
            string url = $"{UrlTimeLine}/{id}?key={ApiKey}";
            WeatherData results = _rs.GetWeatherData(url).Result;

            return View(results);
        }

        public IActionResult WeatherDataDynamicPeriod(string id, string period)
        {
            string url = $"{UrlTimeLine}/{id}/{period}?key={ApiKey}";
            WeatherData results = _rs.GetWeatherData(url).Result;

            return View(results);
        }

        public IActionResult WeatherDataByDateRange(string id, string from, string to)
        {
            string url = $"{UrlTimeLine}/{id}/{from}/{to}?key={ApiKey}";
            WeatherData results = _rs.GetWeatherData(url).Result;

            return View(results);
        }

        public IActionResult WeatherDataByLongLatRange(string longitude, string latitude)
        {
            string url = $"{UrlTimeLine}/{longitude},{latitude}?key={ApiKey}";
            WeatherData results = _rs.GetWeatherData(url).Result;

            return View(results);
        }

        public IActionResult WeatherDataBySpecificTime(string id, string specificTime)
        {
            var datetime = DateTime.Parse(specificTime);
            var url = $"{UrlTimeLine}/{id}/{datetime:yyyy-MM-ddTHH:mm:ss}?key={ApiKey}";
            WeatherData results = _rs.GetWeatherData(url).Result;

            return View(results);
        }

        public IActionResult WeatherDataByUnixTime(string id, string from, string to)
        {
            string url = $"{UrlTimeLine}/{id}/{from}/{to}?key={ApiKey}";
            WeatherData results = _rs.GetWeatherData(url).Result;

            return View(results);
        }
    }
}