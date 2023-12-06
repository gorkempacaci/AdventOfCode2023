using System;
using Helpers;

namespace Day5
{
  public static class PartA
  {
    static Dictionary<string, (string, List<(long src, long dst, long cnt)>)> sToDest = new();

    public static void MainA()
    {
      string[] lines = File.ReadAllLines("input.txt");

      var seeds = lines.First().Split(':')[1].Trim().Split(' ').Select(long.Parse).ToArray();

      string sourceName = "";
      string destinationName;

      foreach (string line in lines.Skip(2))
      {
        if (line.EndsWith(" map:"))
        {
          sourceName = line.Split(' ')[0].Split('-')[0].Trim();
          destinationName = line.Split(' ')[0].Split('-')[2].Trim();
          sToDest.Add(sourceName, (destinationName, new()));
          continue;
        } else
        if (string.IsNullOrEmpty(line))
        {
          sourceName = "";
          destinationName = "";
          continue;
        } else
        {
          var nums = line.Trim().Split(' ').Select(long.Parse).ToArray();
          long destinationPos = nums[0];
          long sourcePos = nums[1];
          long count = nums[2];
          sToDest[sourceName!].Item2.Add((sourcePos, destinationPos, count));
        }
      }

      var locs = seeds.Select(s => lookItUp("seed", s, s));

      var final = locs.OrderBy(l => l).First();

      Console.WriteLine(final);
    }

    static (string dstName, long dstN, long origN) lookItUp(string srcName, long srcN, long origN)
    {
      if (sToDest.ContainsKey(srcName))
      {
        var l = sToDest[srcName];
        var m = l.Item2.Find(e => e.src <= srcN && e.src + e.cnt > srcN);
        if (m == default)
        {
          return lookItUp(l.Item1, srcN, origN);
        }
        else
        {
          long dstN = m.dst + (srcN - m.src);
          return lookItUp(l.Item1, dstN, origN);
        }
      }
      else return (srcName, srcN, origN);
    }

  }
}

