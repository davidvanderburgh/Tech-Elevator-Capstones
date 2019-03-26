using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveyResultSqlDAL : ISurveyResultSqlDAL
    {
        private string connectionString;
        private const string SQL_GETVOTESPERPARK =
            "SELECT survey_result.parkCode, COUNT(survey_result.parkCode) AS votes, park.* " +
            "FROM survey_result " +
            "JOIN park " + 
            "ON park.parkCode = survey_result.parkCode " +
            "GROUP BY survey_result.parkCode, park.parkName, park.parkCode, park.state, park.acreage, park.elevationInFeet, " +
            "park.milesOfTrail, park.numberOfCampsites, park.climate, park.yearFounded, park.annualVisitorCount, " + 
            "park.inspirationalQuote, park.inspirationalQuoteSource, park.parkDescription, park.entryFee, " +
            "park.numberOfAnimalSpecies " +
            "ORDER BY votes DESC, parkName ASC;";
        private const string SQL_ADDSURVEYRESULT =
            "INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) " +
            "VALUES (@parkCode, @emailAddress, @state, @activityLevel); " +
            "SELECT CAST(SCOPE_IDENTITY() as int);";

        public SurveyResultSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<SurveyResult> GetVotesPerPark()
        {
            List<SurveyResult> results = new List<SurveyResult>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_GETVOTESPERPARK, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SurveyResult result = new SurveyResult
                    {
                        Park = new Park
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
                        },
                        Votes = Convert.ToInt32(reader["votes"])
                    };
                    results.Add(result);
                }
            }

            return results;
        }

        public int AddSurveyResult(Survey survey)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(SQL_ADDSURVEYRESULT, conn);
                command.Parameters.AddWithValue("@parkCode", survey.FavoriteNationalPark);
                command.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                command.Parameters.AddWithValue("@state", survey.StateOfResidence);
                command.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                return (int)command.ExecuteScalar();
            }
        }
    }
}
