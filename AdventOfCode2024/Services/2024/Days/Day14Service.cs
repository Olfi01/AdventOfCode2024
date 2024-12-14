using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day14Service(IInputService inputService) : SingleDayService(inputService, 2024, 14)
    {
        private static readonly Regex RobotInput = new(@"p=(?<x>\d+),(?<y>\d+) v=(?<dx>-?\d+),(?<dy>-?\d+)");

        public async Task<long> Part1(int width = 101, int height = 103)
        {
            var robots = (await InputService.GetInputAsList(year, day)).Select(ReadRobotInput);
            robots = robots.Select(r => MoveForSeconds(r, 100, width, height));
            var quad1 = robots.Count(r => r.x < width / 2 && r.y < height / 2);
            var quad2 = robots.Count(r => r.x > width / 2 && r.y < height / 2);
            var quad3 = robots.Count(r => r.x < width / 2 && r.y > height / 2);
            var quad4 = robots.Count(r => r.x > width / 2 && r.y > height / 2);
            return quad1 * quad2 * quad3 * quad4;
        }

        private static (int x, int y, int dx, int dy) MoveForSeconds((int x, int y, int dx, int dy) r, int t, int width, int height)
        {
            int x = (r.x + t * r.dx) % width;
            if (x < 0) x += width;
            int y = (r.y + t * r.dy) % height;
            if (y < 0) y += height;
            return (x, y, r.dx, r.dy);
        }

        private (int x, int y, int dx, int dy) ReadRobotInput(string input)
        {
            var match = RobotInput.Match(input);
            return (int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["y"].Value), int.Parse(match.Groups["dx"].Value), int.Parse(match.Groups["dy"].Value));
        }

        public async Task<long> Part2(int width = 101, int height = 103)
        {
            var robots = (await InputService.GetInputAsList(year, day)).Select(ReadRobotInput);
            robots = robots.Select(r => MoveForSeconds(r, Random.Shared.Next(10403), width, height));   // With a LOT of luck the robots will have formed your tree.
            throw new Exception("This task fucking sucked. I made an entirely separate application to visualize the bots and clicked through about 2000 images, " +
                "then finally with the help of reddit found a pattern that I exploited to find the solution " +
                "(more precisely two patterns, one every 103 seconds and one every 101, where both of those intersected I found my solution).");
        }
    }
}
