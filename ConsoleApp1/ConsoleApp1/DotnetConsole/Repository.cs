using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DotnetConsole
{
  public class Repository<T>: IDisposable where T: class
  {
    private PracticeContext context =  null;

    public DbSet<T> Entities => this.context.Set<T>();
    public IQueryable<T> Table => this.Entities;

    public Repository(PracticeContext context)
    {
      this.context = context;
    }

    public T Insert(T entity)
    {
      try
      {
        T t = this.Entities.Add(entity);
        this.context.SaveChanges();
        return t;
      }
      catch (DbEntityValidationException dbEx)
      {
        var msg = string.Empty;

        foreach (var validationErrors in dbEx.EntityValidationErrors)
        {
          foreach (var validationError in validationErrors.ValidationErrors)
          {
            msg += string.Format("Property: {0} Error: {1}{2}", validationError.PropertyName, validationError.ErrorMessage, Environment.NewLine);
          }
        }

        var fail = new Exception(msg, dbEx);
        //Debug.WriteLine(fail.Message, fail);
        throw fail;
      }
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }
  }
}
