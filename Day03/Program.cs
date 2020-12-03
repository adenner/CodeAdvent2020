using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace Day03
{
    class Program
    {
        private static readonly ILogger log = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
        static void Main(string[] args)
        {
            var currentpos = 0;
            var hits = 0;
            foreach (var line in File.ReadAllLines("Data.txt").Select(x=>x.Trim()))
            {
                var current = line.ElementAt(currentpos);
                if (line.ElementAt(currentpos) == '#')
                    hits++;
                currentpos += 3;
                if (currentpos >= line.Length)
                    currentpos -= line.Length;
            }
            log.LogInformation($" We hit {hits} trees");

            
        }
    }
}
