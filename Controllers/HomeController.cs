using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static readonly string ApiKey = "";

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult WeatherData(string id)
        {
            string url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{id}?key={ApiKey}";
            WeatherData results = _rs.GetWeatherData(url).Result;

            return View(results);
        }
    }
}
