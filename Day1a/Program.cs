


public static class Program
{
  public static void Main(string[] args)
  {
    var lines = File.ReadAllLines("input1.txt");
    Console.WriteLine(lines.Sum(l => int.Parse(l.First(c => Char.IsDigit(c)).ToString() + l.Last(c => Char.IsDigit(c)))));

  }
}