using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Day04
{
    class Program
    {
        private static readonly ILogger log = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
        static void Main(string[] args)
        {
            var sb= new StringBuilder();
            var collector = new List<string>();
            foreach (var line in File.ReadAllLines("data.txt"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    collector.Add(sb.ToString());
                    sb.Clear();
                }
                else
                    sb.Append(line.Trim() + ' ');
            }
            collector.Add(sb.ToString());
            var passports=collector.Select(x => x.Trim().Split(" ").Select(y=>y.Split(':')).ToDictionary(z=>z[0],z=>z[1])).ToList();
            var validcount = passports.Where(x => prob1Required.All(x.ContainsKey)).ToList();
            log.LogInformation($"of possible passports {passports.Count} I think {validcount.Count} are valid");
        }

        private static readonly List<string> prob1Required = new List<string>()
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", //"cid" //This is a hack to let us in as northpole ids don't have cid 
        };


    }
}
