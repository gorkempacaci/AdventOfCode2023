

using System.Text.RegularExpressions;

using Helpers;

/*
 Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
 */


public static class Program
{
  public static void Main(string[] args)
  {
    var lines = File.ReadAllLines("input.txt");

    int sum = 0;

    foreach(var line in lines)
    {
      int id = int.Parse(line.Split(":")[0].Split(' ')[1]);
      var draws = line.Split(":")[1].Split(";");
      Dictionary<string, int> nums = new() {  {"red", 0 }, {"green", 0 }, {"blue", 0 } };
      nums["red"] = 0;
      nums["green"] = 0;
      nums["blue"] = 0;
      foreach(var draw in draws)
      {
        var groups = draw.Split(',');
        foreach(var qcolor in groups)
        {
          int q = int.Parse(qcolor.Trim().Split(' ')[0]);
          string c = qcolor.Trim().Split(' ')[1].Trim();
          nums[c] = int.Max(nums[c], q);
        }
      }
      var pow = nums.Values.Aggregate(1, (x, y) => x * y);
      sum += pow;
    }
    Console.WriteLine(sum);

  }

}