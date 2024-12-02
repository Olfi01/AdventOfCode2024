using Xunit;
using AdventOfCode2024.Services._2019.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AdventOfCode2024.Services.Tests;
using Microsoft.Extensions.Configuration;

namespace AdventOfCode2024.Services._2019.Days.Tests
{
    public class Day3ServiceTests
    {
        private readonly Day3Service service;

        private readonly Mock<IInputService> inputServiceMock;
        public Day3ServiceTests()
        {
            inputServiceMock = new Mock<IInputService>();
            service = new Day3Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public async Task Part1Test(string wireA, string wireB, int closestDistance)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<char>())).ReturnsAsync([wireA, wireB]);

            Assert.Equal(closestDistance, await service.Part1());
        }

        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 30)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public async Task Part2Test(string wireA, string wireB, int closestDistance)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<char>())).ReturnsAsync([wireA, wireB]);

            Assert.Equal(closestDistance, await service.Part2());
        }
    }
}