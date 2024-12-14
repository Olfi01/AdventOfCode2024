using Xunit;
using AdventOfCode2024.Services._2024.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2024.Test.Services;
using Moq;

namespace AdventOfCode2024.Services._2024.Days.Tests
{
    public class Day14ServiceTests : DayServiceTestsBase
    {
        private readonly Day14Service service;

        public Day14ServiceTests()
        {
            service = new Day14Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("p=0,4 v=3,-3\np=6,3 v=-1,-3\np=10,3 v=-1,2\np=2,0 v=2,-1\np=0,0 v=1,3\np=3,0 v=-2,-2\np=7,6 v=-1,-3\np=3,0 v=-1,-2\np=9,3 v=2,3\np=7,3 v=-1,2\np=2,4 v=2,-3\np=9,5 v=-3,-3", 12)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 14, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part1(11, 7));
        }
    }
}