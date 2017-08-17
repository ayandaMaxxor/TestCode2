using System;
using TestCode;

namespace TestCodeTests
{
    public class StatsWeightingStub : IStatsWeighting
    {
        // Sample implementation. Please read the instructions supplied with the test for the implementation required.
        public double Apply(double playerWinPercentage, int playerNumberOfMatches)
        {
            if (playerNumberOfMatches < 100) throw new ArgumentException("Put exciting exception message here", "playerNumberOfMatches");
            return 1.0;
        }
    }
}