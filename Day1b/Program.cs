

using System.Text.RegularExpressions;

public static class Program
{
  public static void Main(string[] args)
  {
    var lines = File.ReadAllLines("input1.txt");

    var keys = new[] {
      (txt:"one", value: 1),
      (txt:"two", value: 2),
      (txt:"three", value: 3),
      (txt:"four", value: 4),
      (txt:"five", value: 5),
      (txt:"six", value: 6),
      (txt:"seven", value: 7),
      (txt:"eight", value: 8),
      (txt:"nine", value: 9),
      (txt:"1", value: 1),
      (txt:"2", value: 2),
      (txt:"3", value: 3),
      (txt:"4", value: 4),
      (txt:"5", value: 5),
      (txt:"6", value: 6),
      (txt:"7", value: 7),
      (txt:"8", value: 8),
      (txt:"9", value: 9)
    };

    var ss = lines.Select(l => keys.OrderBy(k => posclip(l.IndexOf(k.txt))).First().value * 10 +
                             keys.OrderBy(k => posclip(strrev(l).IndexOf(strrev(k.txt)))).First().value);
    Console.WriteLine(ss.Sum());

  }

  /// <summary>
  /// reverse a string
  /// </summary>
  static string strrev(string s)
  {
    char[] chars = s.ToCharArray();
    Array.Reverse(chars);
    return new string(chars);
  }

  /// <summary>
  /// If i >=0, i, otherwise maxint
  /// </summary>
  static int posclip(int i)
  {
    if (i >= 0)
      return i;
    else return int.MaxValue;
  }
}