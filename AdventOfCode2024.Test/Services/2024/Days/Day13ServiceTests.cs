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
    public class Day13ServiceTests : DayServiceTestsBase
    {
        private readonly Day13Service service;

        public Day13ServiceTests()
        {
            service = new Day13Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("Button A: X+94, Y+34\nButton B: X+22, Y+67\nPrize: X=8400, Y=5400\n\nButton A: X+26, Y+66\nButton B: X+67, Y+21\nPrize: X=12748, Y=12176\n\nButton A: X+17, Y+86\nButton B: X+84, Y+37\nPrize: X=7870, Y=6450\n\nButton A: X+69, Y+23\nButton B: X+27, Y+71\nPrize: X=18641, Y=10279", 480)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 13, "\n\n")).ReturnsAsync(input.Split("\n\n"));
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("Button A: X+94, Y+34\nButton B: X+22, Y+67\nPrize: X=8400, Y=5400\n\nButton A: X+26, Y+66\nButton B: X+67, Y+21\nPrize: X=12748, Y=12176\n\nButton A: X+17, Y+86\nButton B: X+84, Y+37\nPrize: X=7870, Y=6450\n\nButton A: X+69, Y+23\nButton B: X+27, Y+71\nPrize: X=18641, Y=10279", 875318608908)]
        public async Task Part2Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 13, "\n\n")).ReturnsAsync(input.Split("\n\n"));
            Assert.Equal(expectedOut, await service.Part2());
        }

        [Fact]
        public void ShouldGaussianSolve()
        {
            long[,] matrix = new long[2, 3]
            {
                { 69, 27, 10000000018641 },
                { 23, 71, 10000000010279 }
            };
            Assert.True(Day13Service.GaussianSolve(matrix));
            Assert.Equal(102851800151, matrix[0, 2]);
            Assert.Equal(107526881786, matrix[1, 2]);
        }
    }
}