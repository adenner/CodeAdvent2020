using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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
            var validcount = validateprob1(passports);
            log.LogInformation($"of possible passports {passports.Count} I think {validcount} are valid");
            var validcount2 = validateprob2(passports);
            log.LogInformation( $"Prob2 got {validcount2}");
        }

        private static int validateprob1(IEnumerable<Dictionary<string,string>> passports)
        {
            var prob1Required = new List<string>()
            {
                "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", //"cid" //This is a hack to let us in as northpole ids don't have cid 
            };
            return passports.Count(x => prob1Required.All(x.ContainsKey));
        }

        private static bool TryGetInteger(string key, Dictionary<string, string> passport, out int val)
        {
            if (passport.TryGetValue(key, out string v) && int.TryParse(v, out int i))
            {
                val = i;
                return true;
            }
            val = 0;
            return false;
        }

        private static bool TryGetRegex(string key, string regx, Dictionary<string, string> passport)
        {
            return passport.TryGetValue(key, out string v) && Regex.IsMatch(v, regx);
        }

        private static int validateprob2(IEnumerable<Dictionary<string, string>> passports)
        {
            var filteredset = passports.Where(x =>
            {
                var byrx = TryGetInteger("byr", x, out int byr) && (byr >= 1920 && byr <= 2002);
                var iyrx = TryGetInteger("iyr", x, out int iyr) && (iyr >= 2010 && iyr <= 2020);
                var eyrx = TryGetInteger("eyr", x, out int eyr) && (eyr >= 2020 && eyr <= 2030);
                var hgtx = TryGetRegex("hgt", "^(1[5-9]\\dcm$)|^(19[0-3]cm$)|^(59in$)|^(6\\din$)|^(7[0-6]in$)", x);
                var hclx = TryGetRegex("hcl", "^\\#(\\d|[a-f]){6}$", x);
                var eclx = TryGetRegex("ecl", "^(amb)$|^(blu)$|^(brn)$|^(gry)$|^(grn)$|^(hzl)$|^(oth)$", x);
                var pidx = TryGetRegex("pid", "^\\d{9}$", x);
                return (byrx && iyrx && eyrx && hgtx && hclx && eclx && pidx);
            });

            return filteredset.Count();




        }
       



    }
}
