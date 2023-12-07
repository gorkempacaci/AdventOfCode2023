using System;
using Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day7
{
  public static class PartB
  {

    public static void MainB()
    {
      string[] lines = File.ReadAllLines("input.txt");
      var cardTypes = "AKQT98765432J";
      var ranks = lines.Select(l => (cards: l.Split(' ')[0], bid: int.Parse(l.Split(' ')[1])))
                       .OrderByDescending(h => handOrd(h.cards))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[0]))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[1]))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[2]))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[3]))
                       .ThenByDescending(h => cardTypes.IndexOf(h.cards[4]))
                       .Select((h, i) => (long)(h.bid * (i + 1)))
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
    static int handOrd(string handOrig)
    {
      var hand = string.Join("", handOrig.Where(c => c != 'J'));
      int js = handOrig.Length - hand.Length;
      var gs = hand.GroupBy(c => c).OrderByDescending(g => g.Count()).ToArray();
      int gLength = gs.Length;
      int dom = gs.Any() ? gs[0].Count() + js : js;
      return gLength switch
      {
        0 => 0,
        1 => 0, // AAAAA
        2 => dom == 4 ? 1 : 2, // AA8AA => 1, 23332 => 2
        3 => dom == 3 ? 3 : 4, // TTT98 => 3, 23432 => 4
        4 => 5, // A23A4
        5 => 6,
        _ => throw new Exception("more than 5 cars")
      }; ;
    }

  }
}

