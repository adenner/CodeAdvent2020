using System;

namespace Day05
{
    public class Seat
    {
        public Seat(string s)
        {
            var row = s.Replace("B", "1")
                .Replace("F", "0")
                .Replace("R", "1")
                .Replace("L", "0");
            Id = Convert.ToInt32(row, 2);
        }
        public int Id { get; }
    }
}