using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class DailyWeather
    {
        public string ParkCode { get; set; }
        public int Day { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public string LowString(bool inFahrenheit)
        {
            if (inFahrenheit)
            {
                return Low + "ºF";
            }
            else
            {
                return (int)((5.0/9.0)*(Low - 32)) + "ºC";
            }
        }

        public string HighString(bool inFahrenheit)
        {
            if (inFahrenheit)
            {
                return High + "ºF";
            }
            else
            {
                return (int)((5.0/9.0) * (High - 32)) + "ºC";
            }
        }

        public string ForecastRecommendation
        {
            get
            {
                if (Forecast.ToLower().Contains("snow"))
                {
                    return "Pack your snowshoes";
                }
                else if (Forecast.ToLower().Contains("rain"))
                {
                    return "Pack rain gear and wear waterproof shoes";
                }
                else if (Forecast.ToLower().Contains("thunderstorms"))
                {
                    return "Seek shelter and avoid hiking on exposed ridges";
                }
                else if (Forecast.ToLower().Contains("sunny"))
                {
                    return "Pack sunblock";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string TemperatureRecommendation
        {
            get
            {
                if (High > 75)
                {
                    return "Bring an extra gallon of water";
                }
                else if ((High-Low) > 20)
                {
                    return "Wear breathable layers";
                }
                else if(Low < 20)
                {
                    return "Danger! Cold temperatures could lead to frostbite, hypothermia, and premature death!!";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
