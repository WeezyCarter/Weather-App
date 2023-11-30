using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WeatherResult()
        {
            return View();
        }
        public async Task<IActionResult> WeatherDetail(string City)
        {

            //Assign API KEY which received from OPENWEATHERMAP.ORG
            string appId = "8113fcc5a7494b0518bd91ef3acc074f";
            ResultViewModel rslt = new ResultViewModel();
            //API path with CITY parameter and other parameters.
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    RootObject weatherInfo = JsonConvert.DeserializeObject<RootObject>(apiResponse);
                    rslt.Country = weatherInfo.sys.country;
                    rslt.City = weatherInfo.name;
                    rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                    rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                    rslt.Description = weatherInfo.weather[0].description;
                    rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                    rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                    rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                    rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                    rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                    rslt.WeatherIcon = weatherInfo.weather[0].icon;
                }
                return View("WeatherResult", rslt);

            }
        }

    }
}
