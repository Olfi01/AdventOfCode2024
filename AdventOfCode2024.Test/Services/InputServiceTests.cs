using Xunit;
using AdventOfCode2024.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AdventOfCode2024.Services.Tests
{
    public class InputServiceTests
    {
        private readonly InputService inputService;

        public InputServiceTests()
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets<InputServiceTests>().Build();
            inputService = new InputService(configuration.GetSection("SessionToken").Value!);
        }

        [Fact]
        public async Task ShouldDownloadInput()
        {
            string response = await inputService.GetInputAsString(2019, 1);
            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }
    }
}