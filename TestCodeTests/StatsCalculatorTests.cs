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
            // Do assertions that result is null here i.e. result.ShouldBeNull() or Assert.IsNull(result). Choose what ever method your preferred test framework uses
        }
        [Test]
        public void ShouldReturnPlayerWithPlayerNumber1()
        { 
            var result = sut.PlayerByPlayerNumber(1);

            Assert.That(result.Name.Equals("Todor Aleksiev"));
            // Do assertions that result is null here i.e. result.ShouldBeNull() or Assert.IsNull(result). Choose what ever method your preferred test framework uses
        }
        [Test]
        public void ShouldReturnNullIfPlayerWithPlayerNumberDoesntExist()
        {
            var result = sut.PlayerByPlayerNumber(10225);

            Assert.IsNull(result, null);
            // Do assertions that result is null here i.e. result.ShouldBeNull() or Assert.IsNull(result). Choose what ever method your preferred test framework uses
        }
    }
}
