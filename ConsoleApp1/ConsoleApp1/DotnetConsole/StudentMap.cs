using System.Data.Entity.ModelConfiguration;

namespace DotnetConsole
{
  public class TeacherMap : EntityTypeConfiguration<Teacher>
  {
    public TeacherMap()
    {
      this.ToTable("Teacher");
      this.Property(s => s.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
      this.Property(s => s.Name);
    }
  }

  public class StudentMap: EntityTypeConfiguration<Student>
  {
    public StudentMap()
    {
      this.ToTable("Student");
      this.Property(s => s.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
      this.Property(s => s.Name);
    }
  }

  public class BranchMap : EntityTypeConfiguration<Branch>
  {
    public BranchMap()
    {
      this.ToTable("Branch");
      this.Property(s => s.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
      this.Property(s => s.Name);
    }
  }
}
