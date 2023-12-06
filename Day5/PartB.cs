using System;
using Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day5
{
  public static class PartB
  {
    static Dictionary<string, (string dstName, List<(long srcStart, long dstStart, long count)> maps)> sourceToDst = new();
    static List<(long start, long count)> seedRanges = new();

    public static void MainB()
    {
      string[] lines = File.ReadAllLines("input.txt");

      var seedsNums = lines.First().Split(':')[1].Trim().Split(' ').Select(long.Parse).ToArray();

      for (int s = 0; s < seedsNums.Length; s += 2)
      {
        seedRanges.Add((seedsNums[s], seedsNums[s + 1]));
      }
      //seedRanges = seedRanges.OrderBy(r => r.start).ToList();

      string sourceName = "";
      string destinationName = "";

      foreach (string line in lines.Skip(2))
      {
        if (line.EndsWith(" map:"))
        {
          sourceName = line.Split(' ')[0].Split('-')[0].Trim();
          destinationName = line.Split(' ')[0].Split('-')[2].Trim();
          sourceToDst.Add(sourceName, (destinationName, new()));
          continue;
        }
        else
        if (string.IsNullOrEmpty(line))
        {
          sourceName = "";
          destinationName = "";
          continue;
        }
        else
        {
          var nums = line.Trim().Split(' ').Select(long.Parse).ToArray();
          long destinationPos = nums[0];
          long sourcePos = nums[1];
          long count = nums[2];
          sourceToDst[sourceName].maps.Add((srcStart: sourcePos, dstStart: destinationPos, count: count));
        }
      }

      // order all lists by source start
      foreach (string key in sourceToDst.Keys)
      {
        sourceToDst[key] = (sourceToDst[key].dstName, sourceToDst[key].maps.OrderBy(m => m.srcStart).ToList());
      }

      List<(string dstName, List<(long start, long count)> dstSearches)> allDispatches = new();
      (string dstName, List<(long start, long count)> dstSearches) dispatch = (dstName: "seed", dstSearches: seedRanges);
      allDispatches.Add(dispatch);
      while(dispatch.dstName != "location")
      {
        dispatch = lookItUp(dispatch.dstName, dispatch.dstSearches);
        allDispatches.Add(dispatch);
      }

      var first = dispatch.dstSearches.OrderBy(r => r.start).First();

      Console.WriteLine(first.start.ToString() ?? "not found");
    }

    /// <summary>
    /// Returns the lowest location number for the given source type and range.
    /// </summary>
    static (string dstName, List<(long start, long count)> dstSearches) lookItUp(string srcName, List<(long start, long count)> searches)
    {
      List<(long start, long count)> dstSearches = new();
      var look = sourceToDst[srcName];

      for (int i = 0; i < searches.Count; i++)
      {
        var search = searches[i];
        if (search.count <= 0)
          break;

        var maps = look.maps.FindAll(e => e.srcStart + e.count - 1 >= search.start && e.srcStart <= search.start + search.count - 1);

        foreach (var map in maps)
        {
          if (search.start < map.srcStart) // gap before the mapped range
          {
            long startGap = search.start;
            long lastGap = map.srcStart - 1;
            long gapCount = lastGap - startGap + 1;
            dstSearches.Add((start: startGap, gapCount));
            search.start = search.start + gapCount;
            search.count = search.count - gapCount;
          }
          long startSrc = Math.Max(map.srcStart, search.start);
          long lastSrc = Math.Min(map.srcStart + map.count - 1, search.start + search.count - 1);
          long count = lastSrc - startSrc + 1;
          long startOffset = search.start - map.srcStart;
          // look up a matching partial map
          dstSearches.Add((start: map.dstStart + startOffset, count: count));
          long newStart = lastSrc + 1; // progress the search range to after this map
          search.count = search.count - count;
          search.start = newStart;
        }
        // if no maps, or gap left over after maps
        if (search.count > 0)
          dstSearches.Add(search);
      }

      return (look.dstName, dstSearches);
    }

  }
}

