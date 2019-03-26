using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL : IWeatherSqlDAL
    {
        private string connectionString;
        private const string SQL_GETWEATHERFORECAST = "SELECT * FROM weather WHERE parkCode = @parkCode ORDER BY fiveDayForecastValue;";

        public WeatherSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<DailyWeather> GetWeatherForecast(string id)
        {
            List<DailyWeather> result = new List<DailyWeather>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GETWEATHERFORECAST, conn);
                cmd.Parameters.AddWithValue("@parkCode", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DailyWeather dailyWeather = new DailyWeather()
                    {
                        Day = Convert.ToInt32(reader["fiveDayForecastValue"]),
                        Forecast = Convert.ToString(reader["forecast"]),
                        High = Convert.ToInt32(reader["high"]),
                        Low = Convert.ToInt32(reader["low"]),
                        ParkCode = Convert.ToString(reader["parkCode"])
                    };

                    result.Add(dailyWeather);
                }
            }
            return result;
        }
    }
}
