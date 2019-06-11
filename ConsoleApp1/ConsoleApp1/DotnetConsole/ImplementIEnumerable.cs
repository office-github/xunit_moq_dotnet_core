using System;
using System.Collections;
using System.Collections.Generic;

namespace DotnetConsole
{
  public class ImplementIEnumerable<T> : IEnumerable<T>
  {
    public static string[] str = { "a", "b", "c", "d", "e" };
    public IEnumerator GetEnumerator()
    {
      return GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) GetEnumerator();
    }
  }

  public class Person
  {
    private Person[] people;
    private static int position = -1;

    public int ID { get; set; }

    public Person(int id)
    {
      this.ID = id;
    }

    public Person(Person[] people)
    {
      this.people = people;
    }

    public Person[] GetEnumerator()
    {
      return this.people;
    }

    public Person Current
    {
      get
      {
        return GetCurrent(position);
      }
    }

    public Person Next
    {
      get
      {
        return GetCurrent(++position);
      }
    }

    public Person Previous
    {
      get
      {
        return GetCurrent(--position);
      }
    }

    public bool Reset
    {
      get
      {
        people = null;
        return true;
      }
    }

    private Person GetCurrent(int position)
    {
      Person person = null;
      
      var result = this.Execute(() =>
      {
        person = people[position];
        return true;
      });

      return person;
    }

    private bool Execute(Func<bool> func)
    {
      try
      {
        return func();
      }
      catch(IndexOutOfRangeException ex)
      {
        Console.WriteLine(ex);
        return false;
      }
    }
  }
}
