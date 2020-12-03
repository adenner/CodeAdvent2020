using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Day1
{
    class Program
    {
        private static readonly ILogger log = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
        static void Main(string[] args)
        {
            var stopwatch=Stopwatch.StartNew();
            var inputs = new Queue<int>( File.ReadAllLines("input.txt").Select(int.Parse));
            var (item1, item2) = Find2020Part1(inputs);
            var multiplier = item1 * item2;
            log.LogInformation($"The two entries are {item1} {item2} and multiply to {multiplier}");
            var (item21, item22, item23) = Find2020Part2(inputs);
            multiplier = item21 * item22 * item23;
            log.LogInformation($"The two entries are {item21} {item22} {item23} and multiply to {multiplier}");
            log.LogInformation($"it took {stopwatch.ElapsedMilliseconds}");
        }

        private static Tuple<int, int> Find2020Part1(Queue<int> inputs)
        {
            var worked = inputs.TryDequeue(out int work);
            if (!worked) return null;
            foreach (var candidate in inputs.Where(candidate => 2020 == candidate + work))
                return Tuple.Create(work, candidate);
            var end = Find2020Part1(inputs);
            return end;
        }
        private static Tuple<int, int, int> Find2020Part2(Queue<int> inputs)
        {
            while (true)
            {
                var worked = inputs.TryDequeue(out int work);
                if (!worked) return null;
                var subset = new Queue<int>(inputs);
                while (subset.Count > 1)
                {
                    var second = subset.Dequeue();
                    foreach (var third in subset.Where(third => work + second + third == 2020)) return Tuple.Create(work, second, third);
                }
            }
        }
    }
}
