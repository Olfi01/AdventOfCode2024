using Moq;

namespace AdventOfCode2024.Services._2024.Days.Tests
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