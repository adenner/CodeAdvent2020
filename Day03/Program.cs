using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    class Program
    {
        private static readonly ILogger log = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();

        private static int Traverse(IEnumerable<string> course, int x, int y)
        {
            y -= 1;
            var xpos = 0;
            var ypos = 0;
            var hits = 0;
            foreach (var line in course)
            {
                if (ypos == 0)
                {
                    if (line.ElementAt(xpos) == '#')
                        hits++;
                    xpos += x;
                }
                ypos++;
                if (xpos >= line.Length)
                    xpos -= line.Length;
                if (ypos > y)
                    ypos = 0;
            }
            return hits;
        }
        static void Main(string[] args)
        {
            var field = File.ReadAllLines("Data.txt");
            var hits = Traverse(field, 3, 1);
            log.LogInformation($"Problem 1:  We hit {hits} trees");
            var workset = new List<Tuple<int, int>>
            {
                Tuple.Create(1, 1),
                Tuple.Create(3, 1),
                Tuple.Create(5, 1),
                Tuple.Create(7, 1),
                Tuple.Create(1, 2)
            }.Select(x => Traverse(field, x.Item1, x.Item2)).ToList();
            long prob2agg = 1;
            workset.ForEach(x=>prob2agg*=(long)x);
            log.LogInformation($"Problem2: We got {prob2agg}");
        }
    }
}
