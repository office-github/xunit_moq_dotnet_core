using System;
using System.Diagnostics;
using System.Linq;

namespace DotnetConsole.Classes
{
  public class ParallelClass
  {
    static int SumDefault(int[] array)
    {
      /*
       *
       * Sum all numbers in the array.
       *
       * */
      return array.Sum();
    }

    static int SumAsParallel(int[] array)
    {
      /*
       *
       * Enable parallelization and then sum.
       *
       * */
      return array.AsParallel().Sum();
    }

    public static void CheckPerformanceArray()
    {
      // Generate array.
      int[] array = Enumerable.Range(0, short.MaxValue).ToArray();

      // Test methods.
      Console.WriteLine(SumAsParallel(array));
      Console.WriteLine(SumDefault(array));

      const int m = 10000;
      var s1 = Stopwatch.StartNew();
      for (int i = 0; i < m; i++)
      {
        SumDefault(array);
      }
      s1.Stop();
      var s2 = Stopwatch.StartNew();
      for (int i = 0; i < m; i++)
      {
        SumAsParallel(array);
      }
      s2.Stop();
      Console.WriteLine(((double)(s1.Elapsed.TotalMilliseconds * 1000000) /
          m).ToString("0.00 ns"));
      Console.WriteLine(((double)(s2.Elapsed.TotalMilliseconds * 1000000) /
          m).ToString("0.00 ns"));
      Console.Read();
    }

    public static void CheckPerformanceDbCall()
    {
      const int m = 10000;

      var s1 = Stopwatch.StartNew();
      DataBaseCall.GetStudents();
      s1.Stop();

      var s2 = Stopwatch.StartNew();
      DataBaseCall.GetStudentsOrderByAsParallel();
      s2.Stop();

      var s3 = Stopwatch.StartNew();
      DataBaseCall.GetStudentsAsParallelOrderBy();
      s3.Stop();

      Console.WriteLine("Normal Fetch Time Students : " + ((double)(s1.Elapsed.TotalMilliseconds * 1000000) /
          m).ToString("0.00 ns"));
      Console.WriteLine("Order By Then AsParallel Fetch Time Students : " + ((double)(s2.Elapsed.TotalMilliseconds * 1000000) /
          m).ToString("0.00 ns"));
      Console.WriteLine("AsParallel Then Order By Fetch Time Students : " + ((double)(s3.Elapsed.TotalMilliseconds * 1000000) /
          m).ToString("0.00 ns"));
    }
  }
}
