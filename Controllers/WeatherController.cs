using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovie.Models;
using System.Net.Http;

namespace MvcMovie.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public WeatherController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // get my data and then pass to the view of Weather
            //https://localhost:5006/WeatherForecast

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            IEnumerable<WeatherModel> weatherList = null;
            //GET request
            using(var client = new HttpClient(httpClientHandler)) {
            
                // this should be in a separate file
                client.BaseAddress = new Uri("https://localhost:5006");
               
                var responseTask = client.GetAsync("weatherforecast");
            
                
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode) {
                    var readJob = result.Content.ReadAsAsync<IList<WeatherModel>>();
                    
                    readJob.Wait();

                    weatherList = readJob.Result;
                } else {
                    
                    _logger.LogInformation("Got No Data :(");
                    _logger.LogInformation("STATUS CODE: " + result.StatusCode.ToString());           
                    weatherList = Enumerable.Empty<WeatherModel>();
                    ModelState.AddModelError(string.Empty, "No Weather Forcast Found!");    
                }
            }
            return View(weatherList);
        }

       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
