using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging.Console;

namespace Day02
{
    class Program
    {
        private static readonly ILogger log = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();

        static void Main(string[] args)
        {
            var dataset= File.ReadAllLines("data.txt").AsParallel().Select(x =>
            {
                    var elements = x.Split(' ');
                    var maxmin = elements[0].Split('-').Select(int.Parse);
                    var min = maxmin.First();
                    var max = maxmin.Last();
                    var target = elements[1].First();
                    var password = elements[2];
                    return (min, max, target, password);
            });
            var prob1=dataset.Select(x =>
            {
                var passwordset = x.password.GroupBy(y => y).ToDictionary(y => y.Key, y => y.Count());
                var targetexists = passwordset.TryGetValue(x.target, out var targetcount);
                var valid = (targetexists && (targetcount >= x.min && targetcount <= x.max));
                return valid;
            });
            var count = prob1.Count(x => x);
            log.LogInformation($"Problem 1 Got {count} valid passwords.");

            var prob2 = dataset.Select(x =>
            {
                var first = x.password.ElementAtOrDefault(x.min-1);
                var second = x.password.ElementAtOrDefault<char>(x.max-1);

                var firstmatch = first == x.target;
                var secondmatch = second == x.target;
                var matchcount = (firstmatch ? 1 : 0) + (secondmatch ? 1 : 0);
                return matchcount==1;
            }).Count(x => x);

            log.LogInformation($"Problem 2 got {prob2} valid passwords");


        }
    }
}
