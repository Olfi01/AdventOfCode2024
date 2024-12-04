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
    public class Day4ServiceTests : DayServiceTestsBase
    {
        private Day4Service service;

        public Day4ServiceTests()
        {
            service = new Day4Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("MMMSXXMASM\nMSAMXMSMSA\nAMXSXMAAMM\nMSAMASMSMX\nXMASAMXAMM\nXXAMMXXAMA\nSMSMSASXSS\nSAXAMASAAA\nMAMMMXMMMM\nMXMXAXMASX", 18)]
        public async Task Part1Test(string input, int num)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 4, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(num, await service.Part1());
        }

        [Theory]
        [InlineData("MMMSXXMASM\nMSAMXMSMSA\nAMXSXMAAMM\nMSAMASMSMX\nXMASAMXAMM\nXXAMMXXAMA\nSMSMSASXSS\nSAXAMASAAA\nMAMMMXMMMM\nMXMXAXMASX", 9)]
        public async Task Part2Test(string input, int num)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 4, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(num, await service.Part2());
        }
    }
}