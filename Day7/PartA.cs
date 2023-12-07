using System;
using Helpers;

namespace Day7
{
  public static class PartA
  {

    public static void MainA()
    {
      string[] lines = File.ReadAllLines("input.txt");
      var cardTypes = "AKQJT98765432";
      var ranks = lines.Select(l => (cards: l.Split(' ')[0], bid: int.Parse(l.Split(' ')[1])))
                       .OrderByDescending(h => handOrd(h.cards))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[0]))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[1]))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[2]))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[3]))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[4]))
                       .Select((h, i) => (long)(h.bid * (i+1)))
                       .Sum();

      Console.WriteLine(ranks);
    }

    /*
    Five of a kind, AAAAA  c=1
    Four of a kind, AA8AA  c=2
    Full house,     23332  c=2
    Three of a kind,TTT98  c=3
    Two pair,       23432  c=3
    One pair,       A23A4  c=4
    High card,      23456  c=5
     */
    static int handOrd(string hand)
    {
      var gs = hand.GroupBy(c => c).OrderByDescending(g => g.Count()).ToArray();
      return gs.Length switch
      {
        1 => 0, // AAAAA
        2 => gs[0].Count() == 4 ? 1 : 2, // AA8AA => 1, 23332 => 2
        3 => gs[0].Count() == 3 ? 3 : 4, // TTT98 => 3, 23432 => 4
        4 => 5, // A23A4
        5 => 6,
        _ => throw new Exception("more than 5 cars")
      }; ;
    }

  }
}

