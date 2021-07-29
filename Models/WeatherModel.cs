using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class WeatherModel
    {

        //{"date":"2021-07-30T08:25:36.6300693+01:00","temperatureC":42,"temperatureF":107,"summary":"Scorching"}
        [DataType(DataType.Date)]
        public DateTime WeatherDate { get; set; }

        public int TemperatureC { get; set; }
        
        public int TemperatureF { get; set; }
         
        public string Summary { get; set; }
       

    }
}