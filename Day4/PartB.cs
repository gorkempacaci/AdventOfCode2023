using System;
using Helpers;

namespace Day4
{
  public static class PartB
  {
    public static void MainB()
    {
      string[] lines = File.ReadAllLines("input.txt");

      long totalExtraCards = 0;
      long[] extras = new long[lines.Length];

      int cid = 0;
      foreach (string line in lines)
      {
        cid++;
        totalExtraCards += extras[cid - 1];
        var w = line.Split(':')[1].Trim().Split('|')[0].Trim().Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(int.Parse);
        var my = line.Split(':')[1].Trim().Split('|')[1].Trim().Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(int.Parse);
        int found = my.Count(x => w.Contains(x));
        for(int i=0; i<found; i++)
        {
          extras[cid + i] += 1 + extras[cid - 1];
        }
        
      }

      Console.WriteLine(totalExtraCards + lines.Length);
    }
  }
}

