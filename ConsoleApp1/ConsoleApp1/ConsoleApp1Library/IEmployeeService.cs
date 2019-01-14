using System.Collections.Generic;

namespace ConsoleApp1Library
{
	public interface IEmployeeService
	{
		bool AddEmployee(Person employee);
		Employee GetEmployee(int id);
		List<Employee> GetEmployees();
	}
}
