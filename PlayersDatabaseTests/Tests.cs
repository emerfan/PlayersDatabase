using NUnit.Framework;
using PlayersDatabase;
using System.Data.SqlClient;
namespace PlayersDatabaseTests
{


    [TestFixture]
    public class Tests
    {
        CRUD crud = new CRUD();


        //Create a new connection to the database
        SqlConnection conn = new SqlConnection("Data Source=lugh4.it.nuigalway.ie;Persist Security Info=True;User ID=msdb2367;Password=msdb2367EM");


        //Mean Distance Test
        [Test]
        public void ShouldCalcMeanDistance()
        {
            double expectedResult = crud.CalcMeanDistance(conn);
            Assert.That(expectedResult, Is.EqualTo(165));

        }

        //Min Distance Test
        [Test]
        public void ShouldCalcMinDistance()
        {
            double expectedResult = crud.CalcMinDistance(conn);
            Assert.That(expectedResult, Is.EqualTo(128));

        }

        //Max Distance Test
        [Test]
        public void ShouldCalcMaxDistance()
        {
            double expectedResult = crud.CalcMaxDistance(conn);
            Assert.That(expectedResult, Is.EqualTo(210));

        }


        //Mean Speed Test
        [Test]
        public void ShouldCalcMeanSpeed()
        {
            double expectedResult = crud.CalcMeanSpeed(conn);
            Assert.That(expectedResult, Is.EqualTo(8.5));

        }

        //Min Speed Test
        [Test]
        public void ShouldCalcMinSpeed()
        {
            double expectedResult = crud.CalcMinSpeed(conn);
            Assert.That(expectedResult, Is.EqualTo(1));

        }

        //Max Speed Test
        [Test]
        public void ShouldCalcMaxSpeed()
        {
            double expectedResult = crud.CalcMaxSpeed(conn);
            Assert.That(expectedResult, Is.EqualTo(24));

        }
    }
}