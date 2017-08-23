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

            Assert.AreEqual(4, result.Count());

        }
        [Test]
        public void ShouldReturnAnEmptyList()
        {
            var result = sut.TeamWinPercentage(50);
            var teamValue = new List<TeamValue>();

            Assert.AreEqual(teamValue, result );

        }
        [Test]
        public void ShouldReturn0WeightingForTeam3()
        {
            var result = sut.TeamWinPercentage().Single(x => x.Name == "We play for food Team Value");
           
            Assert.AreEqual(0,result.PlayerWeighting);

        }
        [Test]
        public void ShouldSortResultsInDescendingOrderByTeamWinPercentage()
        {
            var result = sut.TeamWinPercentage().ToArray();
            var count = 0;

            for (var i = 0; i < result.Count(); i++)
            {
                if (i < result.Count() - 1)
                {
                    if (result[i].TeamWinsPercentage > result[i + 1].TeamWinsPercentage)
                    {
                        count++;
                    }
                }
            }

            Assert.AreEqual(3, count);

        }
    }
}
