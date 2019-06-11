using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace DotnetConsole
{
  public class DataBaseCall
  {
    public static List<Student> Students
    {
      get
      {
        using(PracticeContext context = new PracticeContext())
        {
          Repository<Student> repository = new Repository<Student>(context);
          return repository.Table.ToList();
        }
      }
    }

    public static void Insert(Student student)
    {
      using (PracticeContext context = new PracticeContext())
      {
        Repository<Student> repository = new Repository<Student>(context);
        repository.Insert(student);
      }
    }

    public static List<Branch> Branches
    {
      get
      {
        using (PracticeContext context = new PracticeContext())
        {
          Repository<Branch> repository = new Repository<Branch>(context);
          return repository.Table.ToList();
        }
      }
    }

    public static void Insert(Branch branch)
    {
      using (PracticeContext context = new PracticeContext())
      {
        Repository<Branch> repository = new Repository<Branch>(context);
        repository.Insert(branch);
      }
    }

    public static List<Student> GetStudents()
    {
      return Execute((context) =>
      {
        var students = context.Students.OrderBy(s => s.Name).ToList();

        if (students != null && students.Count() > 0)
        {
          Console.WriteLine($"Student ID : Student Name");
          foreach (Student student in students)
          {
            Console.WriteLine($"{student.ID} : {student.Name}");
          }
        }

        return students;
      });
    }

    public static List<Student> GetStudentsOrderByAsParallel()
    {
      return Execute((context) =>
      {
        var students = context.Students.OrderBy(s => s.Name).AsParallel().ToList();

        if (students != null && students.Count() > 0)
        {
          Console.WriteLine($"Student ID : Student Name");
          foreach (Student student in students)
          {
            Console.WriteLine($"{student.ID} : {student.Name}");
          }
        }

        return students;
      });
    }

    public static List<Student> GetStudentsAsParallelOrderBy()
    {
      return Execute((context) =>
      {
        var students = context.Students.AsParallel().OrderBy(s => s.Name).ToList();

        if (students != null && students.Count() > 0)
        {
          Console.WriteLine($"Student ID : Student Name");
          foreach (Student student in students)
          {
            Console.WriteLine($"{student.ID} : {student.Name}");
          }
        }

        return students;
      });
    }

    public static Student SearchStudentByIEnumerable()
    {
      Console.WriteLine("Sure to Search Student? Yes or No");
      string yesOrNo = Console.ReadLine().ToLower();

      if (yesOrNo != "yes")
        return null;

      return Execute((context) =>
      {
        IEnumerable<Student> students = context.Students.AsEnumerable();
        Console.WriteLine("Student Id?");
        int id = Convert.ToInt32(Console.ReadLine());
        Student student = students.FirstOrDefault(s => s.ID == id);
        Console.WriteLine($"{student?.ID} : {student?.Name}");
        return student;
      });
    }

    public static Student SearchStudentByIQuerable()
    {
      Console.WriteLine("Sure to Search Student? Yes or No");
      string yesOrNo = Console.ReadLine().ToLower();

      if (yesOrNo != "yes")
        return null;

      return Execute((context) =>
      {
        IEnumerable<Student> students = context.Students.AsQueryable();
        Console.WriteLine("Student Id?");
        int id = Convert.ToInt32(Console.ReadLine());
        Student student = students.FirstOrDefault(s => s.ID == id);
        Console.WriteLine($"{student.ID} : {student.Name}");
        return student;
      });
    }

    public static Student SearchStudentByToList()
    {
      Console.WriteLine("Sure to Search Student? Yes or No");
      string yesOrNo = Console.ReadLine().ToLower();

      if (yesOrNo != "yes")
        return null;

      return Execute((context) =>
      {
        IEnumerable<Student> students = context.Students.ToList();
        Console.WriteLine("Student Id?");
        int id = Convert.ToInt32(Console.ReadLine());
        Student student = students.FirstOrDefault(s => s.ID == id);
        Console.WriteLine($"{student.ID} : {student.Name}");
        return student;
      });
    }

    public static bool CreateStudent()
    {
      return Execute((context) => {
        string yesOrNo = "No";
        Console.WriteLine("Sure to Create Student? Yes or No");
        yesOrNo = Console.ReadLine().ToLower();

        if (yesOrNo != "yes")
          return false;

        List<Student> students = new List<Student>();
        do
        {
          Console.WriteLine("Student Name?");
          students.Add(new Student
          {
            Name = Console.ReadLine()
          });

          Console.WriteLine("Add More Student? Yes or No");
          yesOrNo = Console.ReadLine().ToLower();
        }
        while (yesOrNo == "yes");

        context.Students.AddRange(students);
        context.SaveChanges();
        return true;
      });
    }

    private static TResult Execute<TResult>(Func<PracticeContext, TResult> func)
    {
      try
      {
        return func(new PracticeContext());
      }
      catch(SqlException ex)
      {
        Console.WriteLine(ex);
        throw new Exception("Error Occured.");
      }
    }
  }

  public class PracticeContext: DbContext
  {
    public PracticeContext(): base(){
    }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        modelBuilder.Configurations.AddFromAssembly(assembly);
      }
    }
  }
}
