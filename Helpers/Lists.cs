using System;
namespace Helpers
{
  public static class Lists
  {
    public static List<T> FromNullables<T>(params T?[] nullables)
    {
      List<T> lst = new();
      foreach (var t in nullables)
        if (t != null)
          lst.Add(t);
      return lst;
    }
  }
}

