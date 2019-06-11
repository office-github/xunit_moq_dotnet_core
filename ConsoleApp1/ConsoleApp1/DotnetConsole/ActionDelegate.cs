using System;

namespace DotnetConsole
{
  public class ActionDelegate
  {
    public static int Add(int a, int b)
    {
      Action<int, int> action = null;
      int c = 0;
      action = (x, y) =>
      {
        c = x + y;
      };

      action(a, b);

      return c;
    }

    public static void Add(Action<int, int> action)
    {
      Console.WriteLine("Enter a & b:");
      int a = Convert.ToInt32(Console.ReadLine());
      int b = Convert.ToInt32(Console.ReadLine());
      action(a, b);
    }

    public int AddInt(int a, int b)
    {
      return a + b;
    }

    public static Result Execute<FirstParam, SecondParam, Result>(Func<FirstParam, SecondParam, Result> func) where FirstParam : new() where SecondParam : new()
    {
      return func(new FirstParam(), new SecondParam());
    }
  }

  class Object1
  {
    public int Get
    {
      get
      {
        Console.WriteLine("Enter a:");
        int a = Convert.ToInt32(Console.ReadLine());
        return a;
      }
    }
  }

  class Object2
  {
    public int Get
    {
      get
      {
        Console.WriteLine("Enter b:");
        int b = Convert.ToInt32(Console.ReadLine());
        return b;
      }
    }
  }
}
