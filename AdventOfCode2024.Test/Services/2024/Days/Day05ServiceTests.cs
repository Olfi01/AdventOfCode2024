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
    public class Day05ServiceTests : DayServiceTestsBase
    {
        private Day05Service service;

        public Day05ServiceTests()
        {
            service = new Day05Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("47|53\n97|13\n97|61\n97|47\n75|29\n61|13\n75|53\n29|13\n97|29\n53|29\n61|53\n97|53\n61|29\n47|13\n75|47\n97|75\n47|61\n75|61\n47|29\n75|13\n53|13\n\n75,47,61,53,29\n97,61,53,29,13\n75,29,13\n75,97,47,61,53\n61,13,29\n97,13,75,29,47", 143)]
        public async Task Part1Test(string input, int expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 5, "\n\n")).ReturnsAsync(input.Split("\n\n"));
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("47|53\n97|13\n97|61\n97|47\n75|29\n61|13\n75|53\n29|13\n97|29\n53|29\n61|53\n97|53\n61|29\n47|13\n75|47\n97|75\n47|61\n75|61\n47|29\n75|13\n53|13\n\n75,47,61,53,29\n97,61,53,29,13\n75,29,13\n75,97,47,61,53\n61,13,29\n97,13,75,29,47", 123)]
        public async Task Part2Test(string input, int expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 5, "\n\n")).ReturnsAsync(input.Split("\n\n"));
            Assert.Equal(expectedOut, await service.Part2());
        }
    }
}