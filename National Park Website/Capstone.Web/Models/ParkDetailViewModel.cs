using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkDetailViewModel
    {
        public Park Park { get; set; }
        public List<DailyWeather> DailyWeather { get; set; }
        public bool InFahrenheit { get; set; } = true;
    }
}
