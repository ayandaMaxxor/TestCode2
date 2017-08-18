using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestCode;
using TestCode.Models;

namespace TestCodeTests
{
    [TestFixture]
    public class StatsCalculatorTests
    {
        private StatsCalculator sut;
        [SetUp]
        public void SettingUp()
        {
             sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
        }
        [Test]
        public void ShouldReturnNullForPlayerNumberLessOrEqualTo0()
        {
            var result = sut.PlayerByPlayerNumber(0);

            Assert.IsNull(result, null);
            
        }
        [Test]
        public void ShouldReturnPlayerWithPlayerNumber1()
        { 
            var result = sut.PlayerByPlayerNumber(1);

            Assert.That(result.Name.Equals("Todor Aleksiev"));
           
        }
        [Test]
        public void ShouldReturnNullIfPlayerWithPlayerNumberDoesntExist()
        {
            var result = sut.PlayerByPlayerNumber(10225);

            Assert.IsNull(result, null);
            
        }
        [Test]
        public void ShouldReturnTeamValue()
        {
            var result = sut.TeamWinPercentage(1);

            Assert.IsNotNull(result);
            
        }
        [Test]
        public void ShouldReturnListOfAllTeamWithAddedTeamValue()
        {
            var result = sut.TeamWinPercentage();

            Assert.AreEqual(result.Count(), 4);

        }
        [Test]
        public void ShouldReturnAnEmptyList()
        {
            var result = sut.TeamWinPercentage(50);
            var teamValue = new List<TeamValue>();

            Assert.AreEqual(result, teamValue);

        }
    }
}
