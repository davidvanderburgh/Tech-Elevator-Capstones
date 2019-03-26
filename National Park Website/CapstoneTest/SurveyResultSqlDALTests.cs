using Capstone.Web.DAL;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;

namespace CapstoneTest
{
    [TestClass]
    public class SurveyResultSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";
        ISurveyResultSqlDAL surveyResultSqlDAL; 

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            surveyResultSqlDAL = new SurveyResultSqlDAL(connectionString);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd;

                //make a test park
                cmd = new SqlCommand(
                    "INSERT INTO park (parkCode, parkName, state, acreage, elevationInFeet, milesOfTrail, numberOfCampsites, " +
                    "climate, yearFounded, annualVisitorCount, inspirationalQuote, inspirationalQuoteSource, " +
                    "parkDescription, entryFee, numberOfAnimalSpecies) VALUES ('TEST', 'test park', 'TT', 1000, 1000, 1, 3, " +
                    "'hot', 1985, 2, 'so wow', 'doge', 'very big park', 3, 10);", connection);
                cmd.ExecuteNonQuery();

                //make a test weather
                cmd = new SqlCommand(
                    "INSERT INTO weather (parkCode, fiveDayForecastValue, low, high, forecast) VALUES ('TEST', 1, 0, 32, 'cloudy');", connection);
                cmd.ExecuteNonQuery();

                //make 3 test votes
                cmd = new SqlCommand(
                    "INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) " +
                    "VALUES ('TEST', 'test@test.com', 'TT', 'sedentary');", connection);
                cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }
        }

        /*
        * CLEANUP
        * Rollback the existing transaction.
        */
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void AddSurveyResultTests()
        {
            //Arrange
            Survey survey = new Survey()
            {
                ActivityLevel = "sedentary",
                EmailAddress = "test@test.com",
                FavoriteNationalPark = "TEST",
                StateOfResidence = "TT"
            };

            //Act
            int surveyId = surveyResultSqlDAL.AddSurveyResult(survey);

            //Assert
            Assert.AreNotEqual(0, surveyId);
        }

        [TestMethod]
        public void GetVotesPerParkTests()
        {
            //Arrange
            //3 test votes added in initialize method

            //Act
            List<SurveyResult> results =  surveyResultSqlDAL.GetVotesPerPark();
            int numberOfVotes = 0;

            foreach (SurveyResult result in results)
            {
                if (result.Park.ParkCode == "TEST")
                {
                    numberOfVotes = result.Votes;
                }
            }

            //Assert
            Assert.AreNotEqual(0, results.Count);
            Assert.AreEqual(3, numberOfVotes);
        }
    }
}
