using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkSqlDAL
    {
        private string connectionString;
        private const string SQL_GETALLPARKS = "SELECT * FROM park ORDER BY parkName;";
        private const string SQL_GETPARK = "SELECT * FROM park WHERE parkCode = @parkCode;";

        public ParkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Park GetPark(string parkCode)
        {
            Park result = new Park();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GETPARK, conn);
                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = new Park
                    {
                        Acreage = Convert.ToInt32(reader["acreage"]),
                        AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                        Climate = Convert.ToString(reader["climate"]),
                        Description = Convert.ToString(reader["parkDescription"]),
                        ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                        EntryFee = Convert.ToInt32(reader["entryFee"]),
                        MilesOfTrail = Convert.ToDecimal(reader["milesOfTrail"]),
                        NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]),
                        NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                        ParkCode = Convert.ToString(reader["parkCode"]),
                        ParkName = Convert.ToString(reader["parkName"]),
                        Quote = Convert.ToString(reader["inspirationalQuote"]),
                        QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                        State = Convert.ToString(reader["state"]),
                        YearFounded = Convert.ToInt32(reader["yearFounded"])
                    };                    
                }
            }
            return result;
        }

        public List<Park> GetAllParks()
        {
            List<Park> result = new List<Park>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GETALLPARKS, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Park park = new Park()
                    {
                        Acreage = Convert.ToInt32(reader["acreage"]),
                        AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                        Climate = Convert.ToString(reader["climate"]),
                        Description = Convert.ToString(reader["parkDescription"]),
                        ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                        EntryFee = Convert.ToInt32(reader["entryFee"]),
                        MilesOfTrail = Convert.ToDecimal(reader["milesOfTrail"]),
                        NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]),
                        NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                        ParkCode = Convert.ToString(reader["parkCode"]),
                        ParkName = Convert.ToString(reader["parkName"]),
                        Quote = Convert.ToString(reader["inspirationalQuote"]),
                        QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                        State = Convert.ToString(reader["state"]),
                        YearFounded = Convert.ToInt32(reader["yearFounded"])
                    };

                    result.Add(park);
                }
            }
            return result;
        }

        public List<SelectListItem> GetAllParksSelectList()
        {
            List<SelectListItem> allParksSelectList = new List<SelectListItem>();

            foreach (Park park in GetAllParks())
            {
                SelectListItem item = new SelectListItem(park.ParkName, park.ParkCode);
                allParksSelectList.Add(item);
            }

            return allParksSelectList;
        }
    }
}
