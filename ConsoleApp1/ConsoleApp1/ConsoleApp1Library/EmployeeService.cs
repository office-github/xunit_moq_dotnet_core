using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1Library
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IPeopleService peopleService;

		public List<Employee> EmployeeList { get; set; } = new List<Employee>();

		public EmployeeService(IPeopleService peopleService)
		{
			this.peopleService = peopleService;
		}

		public bool AddEmployee(Person employee)
		{
			if(employee == null || employee.ID <= 0)
			{
				throw new Exception();
			}

			Person personAdded;

			try
			{
				personAdded = this.peopleService.AddPerson(employee);
			}
			catch(InvalidOperationException ex)
			{
				throw ex;
			}

			if (personAdded != null)
			{
				this.EmployeeList.Add(new Employee { ID = personAdded.ID });
				return true;
			}

			return false;
		}

		public Employee GetEmployee(int id)
		{
			try
			{
				return this.GetEmployees().FirstOrDefault(employee => employee.ID == id);
			}
			catch(Exception exception)
			{
				return null;
			}
		}

		public List<Employee> GetEmployees()
		{
			var people = this.peopleService.GetPeople();
			this.EmployeeList.Clear();
			people.ForEach(p => EmployeeList.Add(new Employee { ID = p.ID }));

			return EmployeeList;
		}
	}
}
