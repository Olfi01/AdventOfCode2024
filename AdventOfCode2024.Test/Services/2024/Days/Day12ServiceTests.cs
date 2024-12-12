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
    public class Day12ServiceTests : DayServiceTestsBase
    {
        private readonly Day12Service service;

        public Day12ServiceTests()
        {
            service = new Day12Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("AAAA\nBBCD\nBBCC\nEEEC", 140)]
        [InlineData("RRRRIICCFF\nRRRRIICCCF\nVVRRRCCFFF\nVVRCCCJFFF\nVVVVCJJCFE\nVVIVCCJJEE\nVVIIICJJEE\nMIIIIIJJEE\nMIIISIJEEE\nMMMISSJEEE", 1930)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 12, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("AAAA\nBBCD\nBBCC\nEEEC", 80)]
        [InlineData("RRRRIICCFF\nRRRRIICCCF\nVVRRRCCFFF\nVVRCCCJFFF\nVVVVCJJCFE\nVVIVCCJJEE\nVVIIICJJEE\nMIIIIIJJEE\nMIIISIJEEE\nMMMISSJEEE", 1206)]
        public async Task Part2Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 12, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part2());
        }
    }
}