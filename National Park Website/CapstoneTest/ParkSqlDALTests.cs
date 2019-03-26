using Capstone.Web.DAL;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;

namespace CapstoneTest
{
    [TestClass]
    public class ParkSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";
        IParkSqlDAL parkSqlDAL;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            parkSqlDAL = new ParkSqlDAL(connectionString);

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

                //make 5 test weathers
                for (int i = 1; i <= 5; i++)
                {
                    cmd = new SqlCommand(
                        $"INSERT INTO weather (parkCode, fiveDayForecastValue, low, high, forecast) VALUES ('TEST', ${i}, 0, 32, 'cloudy');", connection);
                    cmd.ExecuteNonQuery();
                }

                //make a test vote
                cmd = new SqlCommand(
                    "INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) " +
                    "VALUES ('TEST', 'test@test.com', 'TT', 'sedentary');", connection);
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
        public void GetParkTests()
        {
            //Arrange
            Park park = new Park(); 

            //Act
            park = parkSqlDAL.GetPark("TEST");

            //Assert
            Assert.AreEqual("TEST", park.ParkCode);
        }

        [TestMethod]
        public void GetAllParksTest()
        {
            //Arrange
            List<Park> parks = new List<Park>();

            //Act
            parks = parkSqlDAL.GetAllParks();

            //Assert
            Assert.AreNotEqual(0, parks.Count);
        }
    }
}
