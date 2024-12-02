using Xunit;
using AdventOfCode2024.Services._2019.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days.Tests
{
    public class Day4ServiceTests
    {
        [Theory]
        [InlineData(111111, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void MeetsPart1CriteriaTest(int number, bool meetsCriteria)
        {
            Assert.Equal(meetsCriteria, Day4Service.MeetsPart1Criteria(number));
        }

        [Theory]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]
        public void MeetsPart2CriteriaTest(int number, bool meetsCriteria)
        {
            Assert.Equal(meetsCriteria, Day4Service.MeetsPart2Criteria(number));
        }
    }
}