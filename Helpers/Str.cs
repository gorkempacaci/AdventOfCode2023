using System;
namespace Helpers
{
  public static class Str
  {
    /// <summary>
    /// Reverse a string
    /// </summary>
    public static string Reversed(this string s)
    {
      char[] chars = s.ToCharArray();
      Array.Reverse(chars);
      return new string(chars);
    }

    /// <summary>
    /// Returns true if the character appears in the given lines, row, and column. Does bounds check.
    /// </summary>
    public static bool IsAt(this char ch, string[] lines, int r, int c)
    {
      if (r >= 0 && c>= 0 && r < lines.Length && c < lines[0].Length)
      {
        if (lines[r][c] == ch)
          return true;
      }
      return false;
    }

    /// <summary>
    /// Returns the integer at given character position. Joins with the previous as well as later characters to form a whole number.
    /// For example, ["h456."].ReadNumberAt(0, 1), "h456.".ReadNumberAt(0, 2), "h456.".ReadNumberAt(0, 3) all return the identical (0, 1, 456).
    /// </summary>
    public static (int r, int c, int n)? ReadNumberAt(this string[] lines, int r, int c)
    {
      if (r<0 || r >lines.Length || c<0 || c>lines[0].Length || !char.IsDigit(lines[r][c]))
        return null;
      while (c > 0 && char.IsDigit(lines[r][c - 1]))
        c--;
      int num = 0;
      while (c < lines[0].Length && char.IsDigit(lines[r][c]))
      {
        num = num * 10 + (lines[r][c] - '0');
        c++;
      }
      return (r, c, num);
    }
  }
}

