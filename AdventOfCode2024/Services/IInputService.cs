using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services
{
    public interface IInputService
    {
        Task<string> GetInputAsString(int year, int day);
        Task<IEnumerable<string>> GetInputAsList(int year, int day, char separator = '\n');
        Task<IEnumerable<int>> GetInputAsIntList(int year, int day, char separator = '\n');
        Task<IEnumerable<long>> GetInputAsLongList(int year, int day, char separator = '\n');
    }
}
