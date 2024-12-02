using Xunit;
using AdventOfCode2024.Services._2019.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AdventOfCode2024.Test.Services;

namespace AdventOfCode2024.Services._2019.Days.Tests
{
    public class Day6ServiceTests : DayServiceTestsBase
    {
        private readonly Day6Service service;

        public Day6ServiceTests()
        {
            service = new Day6Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L", 42)]
        public async Task Part1Test(string input, int expectedOrbits)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2019, 6, It.IsAny<char>())).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOrbits, await service.Part1());
        }

        [Theory]
        [InlineData("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L\nK)YOU\nI)SAN", "D")]
        public void FindCommonRootTest(string input, string expectedCommonRoot)
        {
            var orbits = Day6Service.MapOrbits(input.Split('\n'));
            Assert.Equal(expectedCommonRoot, Day6Service.FindCommonRoot(orbits, "YOU", "SAN"));
        }

        [Theory]
        [InlineData("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L\nK)YOU\nI)SAN", 4)]
        public async Task Part2Test(string input, int expectedOrbits)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2019, 6, It.IsAny<char>())).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOrbits, await service.Part2());
        }
    }
}