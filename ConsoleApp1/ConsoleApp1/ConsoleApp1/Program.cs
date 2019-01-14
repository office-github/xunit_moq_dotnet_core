using ConsoleApp1Library;
using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			//People
			Person person = new Person
			{
				ID = 1
			};

			IPeopleService peopleService = new PeopleService();
			//var people = peopleService.AddPeople(person);
			//Console.WriteLine(people.ID);

			//Employee
			IEmployeeService employeeService = new EmployeeService(peopleService);
			bool isAdded = employeeService.AddEmployee(person);
			Console.WriteLine(isAdded);

			var employees = employeeService.GetEmployees();
			Console.WriteLine(employees.Count);
			Console.ReadKey();
		}
	}
}
