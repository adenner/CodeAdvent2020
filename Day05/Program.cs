using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Day05
{
    class Program
    {
        private static readonly ILogger log = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines("input.txt");
            var seatset = inputs.Select(x => new Seat(x)).Select(x => x.Id);
            log.LogInformation($"Part1: max seat is {seatset.Max()}");
        }
    }
}
