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
    public class Day18ServiceTests : DayServiceTestsBase
    {
        private readonly Day18Service service;

        public Day18ServiceTests()
        {
            service = new(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("5,4\n4,2\n4,5\n3,0\n2,1\n6,3\n2,4\n1,5\n0,6\n3,3\n2,6\n5,1\n1,2\n5,5\n2,5\n6,5\n1,4\n0,4\n6,4\n1,1\n6,1\n1,0\n0,5\n1,6\n2,0", 22)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 18, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part1(7, 12));
        }

        [Theory]
        [InlineData("5,4\n4,2\n4,5\n3,0\n2,1\n6,3\n2,4\n1,5\n0,6\n3,3\n2,6\n5,1\n1,2\n5,5\n2,5\n6,5\n1,4\n0,4\n6,4\n1,1\n6,1\n1,0\n0,5\n1,6\n2,0", "6,1")]
        public async Task Part2Test(string input, string expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 18, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part2(7, 13));
        }
    }
}