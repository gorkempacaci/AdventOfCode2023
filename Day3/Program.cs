using Helpers;


public static class Program
{
  public static void Main(string[] args)
  {
    var lines = File.ReadAllLines("input.txt");

    int sum = 0;

    for (int r = 0; r < lines.Length; r++)
    {
      for (int c = 0; c < lines[r].Length; c++)
      {
        if ('*'.IsAt(lines, r, c))
        {
          var numbers = Lists.FromNullables(lines.ReadNumberAt(r - 1, c - 1),
                                                  lines.ReadNumberAt(r - 1, c),
                                                  lines.ReadNumberAt(r - 1, c + 1),
                                                  lines.ReadNumberAt(r, c - 1),
                                                  lines.ReadNumberAt(r, c + 1),
                                                 lines.ReadNumberAt(r + 1, c - 1),
                                                 lines.ReadNumberAt(r + 1, c),
                                                 lines.ReadNumberAt(r + 1, c + 1)).Distinct().ToArray();
          if (numbers.Length > 2)
            Console.WriteLine($"Too many numbers around {r},{c}.");
          if (numbers.Length == 2)
          {
            int x = numbers[0]!.Value.n;
            int y = numbers[1]!.Value.n;
            sum += x * y;
            Console.WriteLine($"{x} * {y}");
          }
        }
      }
    }

    Console.WriteLine(sum);
  }

}