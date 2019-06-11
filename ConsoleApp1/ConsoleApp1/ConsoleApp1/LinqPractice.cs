using ConsoleApp1Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
  public class LinqPractice
  {
    public List<int> Numbers { get; set; } = new List<int>()
    {
      1, 2, 3, 4, 5, 6, 7
    };

    public static string[] Countries { get; set; } = { "Nepal", "India", "China", "Bangladesh" };

    public static void LinqAggregateMethod()
    {
      Console.WriteLine("Aggregate Method");
      Console.WriteLine(Countries.Aggregate((a, b)=> a + "," + b));
    }
  }

  public interface ISomething<T, K>
  {
    List<T> ListA { get; set; }
    List<K> ListB { get; set; }
  }

  public class Something01 : ISomething<string, Person>
  {
    public List<string> ListA { get; set; }
    public List<Person> ListB { get; set; }
  }
}
