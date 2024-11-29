using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services
{
    internal class InputService
    {
        private readonly HttpClient httpClient;
        public InputService(string authToken)
        {
            CookieContainer container = new();
            container.Add(new Cookie("session", authToken));
            HttpClientHandler handler = new() { CookieContainer = container };
            this.httpClient = new HttpClient(handler);
        }

        public string getInputAsString(int year, int day)
        {
            string url = $"https://adventofcode.com/{year}/day/{day}/input";
            return httpClient.GetAsync(url).Result;
        }
    }
}
