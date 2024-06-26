﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherForecast.Models;
using WeatherForecast.Services;
using Microsoft.Extensions.Configuration;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IConfiguration configuration;

        private static string _apiKey;

        private static readonly string UrlTimeLine = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline";

        private RestService RestService { get; set; }

        public HomeController(ILogger<HomeController> logger, IConfiguration iConfig)
        {
            _logger = logger;
            RestService = new RestService();
            configuration = iConfig;
            _apiKey = configuration.GetSection("MySettings").GetSection("ApiKey").Value;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index Request");
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult WeatherData(string id, int overThan = 0, int lessThan = 0, bool? noRain = null, bool? partlyCloudy = null)
        {
            var url = $"{UrlTimeLine}/{id}?key={_apiKey}";
            var results = RestService.GetWeatherData(url).Result;
            var temp = new List<DataPoint>();
            var tempMax = new List<DataPoint>();
            var tempMin = new List<DataPoint>();

            foreach (var day in results.Days)
            {
                temp.Add(new DataPoint(day.DateTime, double.Parse(day.Temp)));
                tempMax.Add(new DataPoint(day.DateTime, double.Parse(day.TempMax)));
                tempMin.Add(new DataPoint(day.DateTime, double.Parse(day.TempMin)));
            }

            ViewBag.Temp = JsonConvert.SerializeObject(temp);
            ViewBag.TempMax = JsonConvert.SerializeObject(tempMax);
            ViewBag.TempMin = JsonConvert.SerializeObject(tempMin);
            var hasFilter = overThan > 0 || lessThan > 0 || noRain == true || partlyCloudy == true;
            var days = results.Days;
            //filter
            days = Filter(overThan, lessThan, noRain, partlyCloudy, days);

            if (hasFilter)
            {
                return PartialView("_DaysTable", days);
            }

            return View(results);
        }

        public IActionResult WeatherDataDynamicPeriod(string id, string period)
        {
            var url = $"{UrlTimeLine}/{id}/{period}?key={_apiKey}";
            var results = RestService.GetWeatherData(url).Result;
            results.Period = period;
            var windSpeed = new List<DataPoint>();
            var windGust = new List<DataPoint>();
            var windDir = new List<DataPoint>();
            if (results.Period != "last24hours" && results.Period != "next24hours")
            {
                foreach (var day in results.Days)
                {
                    windSpeed.Add(new DataPoint(day.DateTime, double.Parse(day.WindSpeed)));
                    windGust.Add(new DataPoint(day.DateTime, double.Parse(day.Windgust)));
                    windDir.Add(new DataPoint(day.DateTime, double.Parse(day.WindDir)));
                }
            }
            else
            {
                foreach (var hour in results.Days[0].Hours)
                {
                    windSpeed.Add(new DataPoint(hour.DateTime, double.Parse(hour.WindSpeed)));
                    windGust.Add(new DataPoint(hour.DateTime, double.Parse(hour.Windgust)));
                    windDir.Add(new DataPoint(hour.DateTime, double.Parse(hour.WindDir)));
                }
            }

            ViewBag.WindSpeed = JsonConvert.SerializeObject(windSpeed);
            ViewBag.WindGust = JsonConvert.SerializeObject(windGust);
            ViewBag.WindDir = JsonConvert.SerializeObject(windDir);

            return View(results);
        }

        public IActionResult WeatherDataByDateRange(string id, string from, string to)
        {
            var url = $"{UrlTimeLine}/{id}/{from}/{to}?key={_apiKey}";
            var results = RestService.GetWeatherData(url).Result;

            var temp = results.Days.Select(day =>
                    new DataRangePoint(day.DateTime, new[] { double.Parse(day.TempMin), double.Parse(day.TempMax) }))
                .ToList();

            ViewBag.Temp = JsonConvert.SerializeObject(temp);
            return View(results);
        }

        public IActionResult WeatherDataByLongLatRange(string longitude, string latitude)
        {
            var url = $"{UrlTimeLine}/{longitude},{latitude}?key={_apiKey}";
            var results = RestService.GetWeatherData(url).Result;
            var dew = results.Days.Select(day => new DataPoint(day.DateTime, double.Parse(day.Dew))).ToList();

            ViewBag.Dew = JsonConvert.SerializeObject(dew);
            results.BingMapApiKey = configuration.GetSection("MySettings").GetSection("BingMapApiKey").Value;
            results.Longitude = double.Parse(longitude);
            results.Latitude = double.Parse(latitude);
            return View(results);
        }

        public IActionResult WeatherDataBySpecificTime(string id, string specificTime)
        {
            var datetime = DateTime.Parse(specificTime);
            var url = $"{UrlTimeLine}/{id}/{datetime:yyyy-MM-ddTHH:mm:ss}?key={_apiKey}";
            var results = RestService.GetWeatherData(url).Result;
            var feelLike = results.Days[0].Hours.Select(hour => new DataPoint(hour.DateTime, double.Parse(hour.FeelsLike))).ToList();

            ViewBag.Feelslike = JsonConvert.SerializeObject(feelLike);
            return View(results);
        }

        public IActionResult WeatherDataByUnixTime(string id, string from, string to)
        {
            var url = $"{UrlTimeLine}/{id}/{from}/{to}?key={_apiKey}";
            var results = RestService.GetWeatherData(url).Result;
            var number = 15;

            if (results.Days.Count < number)
            {
                number = results.Days.Count;
            }

            var temp = results.Days.Take(number).Select(day =>
                    new DataRangePoint(day.DateTime, new[] { double.Parse(day.TempMin), double.Parse(day.TempMax) }))
                .ToList();

            ViewBag.Temp = JsonConvert.SerializeObject(temp);
            return View(results);
        }

        private static List<Days> Filter(int overThan, int lessThan, bool? noRain, bool? partlyCloudy, List<Days> days)
        {
            if (overThan > 0)
            {
                days = days.Where(x => double.Parse(x.Temp) > overThan).ToList();
            }

            if (lessThan > 0)
            {
                days = days.Where(x => double.Parse(x.TempMax) < lessThan).ToList();
            }

            if (noRain == true)
            {
                days = days.Where(x => x.PreCipType == null).ToList();
            }

            if (partlyCloudy == true)
            {
                days = days.Where(x => x.Description.Contains("Partly cloudy")).ToList();
            }

            return days;
        }
    }
}