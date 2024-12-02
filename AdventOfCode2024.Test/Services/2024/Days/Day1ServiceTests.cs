using Xunit;
using AdventOfCode2024.Services._2024.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace AdventOfCode2024.Services._2024.Days.Tests
{
    public class Day1ServiceTests
    {
        private readonly Mock<IInputService> inputServiceMock;
        private readonly Day1Service service;

        public Day1ServiceTests()
        {
            inputServiceMock = new Mock<IInputService>();
            service = new Day1Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("3   4\n4   3\n2   5\n1   3\n3   9\n3   3", 11)]
        public async Task Part1Test(string input, int number)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<char>())).ReturnsAsync(input.Split('\n'));

            Assert.Equal(number, await service.Part1());
        }

        [Theory]
        [InlineData("3   4\n4   3\n2   5\n1   3\n3   9\n3   3", 31)]
        public async Task Part2Test(string input, int number)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<char>())).ReturnsAsync(input.Split('\n'));

            Assert.Equal(number, await service.Part2());
        }
    }
}