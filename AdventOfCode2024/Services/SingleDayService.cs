using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services
{
    public abstract class SingleDayService(IInputService inputService, int year, int day)
    {
        protected IInputService InputService { get; } = inputService;
        protected int year = year;
        protected int day = day;

        protected LoggingService Logger { get; } = new LoggingService();
    }
}
