using System.Collections.Generic;

namespace DotnetConsole.Classes
{
  public class DynamicFromClass
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string EMail { get; set; }
    public string Phone { get; set; }

    public DynamicFromClass Get()
    {
      return new DynamicFromClass
      {
        Id = 1,
        Name = "Bijay Kumar Yadav",
        EMail = "b@b.com",
        Phone = "+977-9801123123"
      };
    }

    public List<DynamicFromClass> GetAll()
    {
      return new List<DynamicFromClass>
      {
        new DynamicFromClass
        {
          Id = 1,
          Name = "Bijay Kumar Yadav",
          EMail = "b@b.com",
          Phone = "+977-9801123123"
        },
        new DynamicFromClass
        {
          Id = 2,
          Name = "Bijay Koirala",
          EMail = "bk@b.com",
          Phone = "+977-9841123123"
        }
      };
    }
  }
}
