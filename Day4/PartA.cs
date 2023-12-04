using System;
using Helpers;

namespace Day4
{
  public static class PartA
  {
    public static void MainA()
    {
      string[] lines = File.ReadAllLines("input.txt");

      int sum = 0;

    
      foreach(string line in lines)
      {
        int reward = 0;
        var w = line.Split(':')[1].Trim().Split('|')[0].Trim().Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(int.Parse);
        var my = line.Split(':')[1].Trim().Split('|')[1].Trim().Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(int.Parse);
        foreach (var m in my)
        {
          if (w.Contains(m))
          {
            if (reward == 0)
              reward = 1;
            else reward = reward * 2;
          }

        }
        sum += reward;
      }


      Console.WriteLine(sum);
    }
  }
}

