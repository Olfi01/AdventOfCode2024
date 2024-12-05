using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services
{
    public class InputService : IInputService
    {
        private readonly HttpClient httpClient;
        public InputService(string authToken)
        {
            CookieContainer container = new();
            container.Add(new Uri("http://adventofcode.com"), new Cookie("session", authToken));
            HttpClientHandler handler = new() { CookieContainer = container };
            this.httpClient = new HttpClient(handler);
        }

        public async Task<string> GetInputAsString(int year, int day)
        {
            string url = $"https://adventofcode.com/{year}/day/{day}/input";
            return await httpClient.GetStringAsync(url);
        }

        public async Task<IEnumerable<string>> GetInputAsList(int year, int day, char separator = '\n')
        {
            return (await GetInputAsString(year, day)).Split(separator).Where(s => !string.IsNullOrEmpty(s));
        }

        public async Task<IEnumerable<string>> GetInputAsList(int year, int day, string separator)
        {
            return (await GetInputAsString(year, day)).Split(separator).Where(s => !string.IsNullOrEmpty(s));
        }

        public async Task<IEnumerable<int>> GetInputAsIntList(int year, int day, char separator = '\n')
        {
            return (await GetInputAsList(year, day, separator)).Select(int.Parse).ToArray();
        }

        public async Task<IEnumerable<long>> GetInputAsLongList(int year, int day, char separator = '\n')
        {
            return (await GetInputAsList(year, day, separator)).Select(long.Parse).ToArray();
        }
    }
}
