using System;

namespace DotnetConsole
{
  public class VariousMethod
  {
    public static bool DynamicParameter<T>(T a, T b, T c) where T: IComparable<T>
    {
      return a.CompareTo(b) == 0 && b.CompareTo(c) == 0 ;
    }
  }
}
