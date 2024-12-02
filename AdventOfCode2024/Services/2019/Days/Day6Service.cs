using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day6Service(IInputService inputService) : SingleDayService(inputService, 2019, 6)
    {
        public async Task<int> Part1()
        {
            var lines = await InputService.GetInputAsList(year, day);
            Dictionary<string, string> orbits = MapOrbits(lines);
            int sum = 0;
            foreach (string o in orbits.Keys)
            {
                string obj = o;
                while (orbits.ContainsKey(obj))
                {
                    sum += 1;
                    obj = orbits[obj];
                }
            }
            return sum;
        }

        public static Dictionary<string, string> MapOrbits(IEnumerable<string> lines)
        {
            Dictionary<string, string> orbits = [];
            foreach (var line in lines)
            {
                string[] split = line.Split(')');
                orbits[split[1]] = split[0];
            }
            return orbits;
        }

        public async Task<int> Part2()
        {
            var lines = await InputService.GetInputAsList(year, day);
            Dictionary<string, string> orbits = MapOrbits(lines);
            string commonRoot = FindCommonRoot(orbits, "YOU", "SAN");
            int transfers = 0;
            string root = orbits["YOU"];
            while (root != commonRoot)
            {
                root = orbits[root];
                transfers++;
            }
            root = orbits["SAN"];
            while (root != commonRoot)
            {
                root = orbits[root];
                transfers++;
            }
            return transfers;
        }

        public static string FindCommonRoot(Dictionary<string, string> orbits, string a, string b)
        {
            string rootA = a;
            while (orbits.ContainsKey(rootA))
            {
                rootA = orbits[rootA];
                string rootB = b;
                if (rootA == rootB) return rootA;
                while (orbits.ContainsKey(rootB))
                {
                    rootB = orbits[rootB];
                    if (rootA == rootB) return rootA;
                }
            }
            throw new Exception();
        }
    }
}
