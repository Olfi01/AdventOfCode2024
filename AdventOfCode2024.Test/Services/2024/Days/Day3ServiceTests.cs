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
    public class Day3ServiceTests : DayServiceTestsBase
    {
        private readonly Day3Service service;

        public Day3ServiceTests()
        {
            service = new Day3Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))\r\n", 161)]
        public async Task Part1Test(string input, int expectedResult)
        {
            inputServiceMock.Setup(s => s.GetInputAsString(2024, 3)).ReturnsAsync(input);
            Assert.Equal(expectedResult, await service.Part1());
        }

        [Theory]
        [InlineData("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", 48)]
        public async Task Part2Test(string input, int expectedResult)
        {
            inputServiceMock.Setup(s => s.GetInputAsString(2024, 3)).ReturnsAsync(input);
            Assert.Equal(expectedResult, await service.Part2());
        }
    }
}