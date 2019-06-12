using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace DotnetConsole
{
  public class DataBaseCall
  {
    public static List<Teacher> GetTeachers(Expression<Func<Teacher, bool>> expression)
    {
      try
      {
        return Execute<List<Teacher>, Teacher>((repository) => {
          return repository.Table.Where(expression).ToList();
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return null;
      }
    }

    public static Teacher Insert(Teacher teacher)
    {
      try
      {
        return Execute<Teacher, Teacher>((repository) => {
          return repository.Insert(teacher);
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return null;
      }
    }

    public static List<Student> GetStudents(Expression<Func<Student, bool>> expression)
    {
      try
      {
        return Execute<List<Student>, Student>((repository) => {
          return repository.Table.Where(expression).ToList();
        });
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex);
        return null;
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
        try
        {
          return Execute<List<Branch>, Branch>((repository) => {
            return repository.Table.ToList();
          });
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
          return null;
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

    private static TResult Execute<TResult, T>(Func<Repository<T>, TResult> func) where T: class
    {
      using(PracticeContext context = new PracticeContext())
      {
        try
        {
          return func(new Repository<T>(context));
        }
        catch(Exception ex)
        {
          Console.WriteLine(ex);
          throw new Exception("Error Occured.");
        }
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
