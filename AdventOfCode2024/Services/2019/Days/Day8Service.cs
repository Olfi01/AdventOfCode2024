using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day8Service(IInputService inputService) : SingleDayService(inputService, 2019, 8)
    {
        public async Task<int> Part1()
        {
            List<int[,]> layers = await ReadLayers();

            var fewestZeroesLayer = layers.MinBy(l => l.Cast<int>().Count(d => d == 0))!;
            return fewestZeroesLayer.Cast<int>().Count(d => d == 1) * fewestZeroesLayer.Cast<int>().Count(d => d == 2);
        }

        private async Task<List<int[,]>> ReadLayers()
        {
            var enumerator = (await InputService.GetInputAsString(year, day)).Select(c => c - '0').GetEnumerator();
            List<int[,]> layers = [];
            while (enumerator.MoveNext())
            {
                var layer = new int[25, 6];
                for (int y = 0; y < 6; y++)
                {
                    for (int x = 0; x < 25; x++)
                    {
                        layer[x, y] = enumerator.Current;
                        if (x != 24 || y != 5) enumerator.MoveNext();
                    }
                }
                layers.Add(layer);
            }

            return layers;
        }

        public async Task Part2()
        {
            List<int[,]> layers = await ReadLayers();
            int[,] image = new int[25, 6];
            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    image[x, y] = 2;
                }
            }

            foreach (var layer in layers)
            {
                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        if (image[x, y] == 2)
                        {
                            image[x, y] = layer[x, y];
                        }
                    }
                }
                if (!image.Cast<int>().Any(d => d == 2)) break;
            }

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    Console.Write(image[x, y] == 0 ? " " : "*");
                }
                Console.WriteLine();
            }
        }
    }
}
