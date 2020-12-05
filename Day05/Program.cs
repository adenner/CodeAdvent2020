using System;
using System.Collections.Generic;
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

            var max = seatset.Max();
            var min = seatset.Min();
            
            log.LogInformation($"Part1: max seat is {max}");

            var seathash=new HashSet<int>(seatset);

            var id = Enumerable.Range(min, max - min + 1).First(x => !seathash.Contains(x));
            log.LogInformation($" part 2 my id is {id}");




        }
    }
}
