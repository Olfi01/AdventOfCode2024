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
    public class Day2ServiceTests
    {

        [Theory]
        [InlineData("7 6 4 2 1", true)]
        [InlineData("1 2 7 8 9", false)]
        [InlineData("9 7 6 2 1", false)]
        [InlineData("1 3 2 4 5", false)]
        [InlineData("8 6 4 4 1", false)]
        [InlineData("1 3 6 7 9", true)]
        public void Part1Test(string input, bool expected)
        {
            var report = input.Split(' ').Select(int.Parse);
            Assert.Equal(expected, Day2Service.IsSafe(report));
        }

        [Theory]
        [InlineData("7 6 4 2 1", true)]
        [InlineData("1 2 7 8 9", false)]
        [InlineData("9 7 6 2 1", false)]
        [InlineData("1 3 2 4 5", true)]
        [InlineData("8 6 4 4 1", true)]
        [InlineData("1 3 6 7 9", true)]
        public void Part2Test(string input, bool expected)
        {
            var report = input.Split(' ').Select(int.Parse);
            Assert.Equal(expected, Day2Service.IsSafeIncludingDampener(report));
        }
    }
}