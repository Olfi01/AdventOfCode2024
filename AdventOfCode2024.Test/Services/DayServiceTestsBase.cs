using AdventOfCode2024.Services;
using Moq;

namespace AdventOfCode2024.Test.Services
{
    public class DayServiceTestsBase
    {
        protected readonly Mock<IInputService> inputServiceMock;

        public DayServiceTestsBase()
        {
            inputServiceMock = new Mock<IInputService>();
        }
    }
}