using ConsoleApp1Library;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject1
{
	public class UnitTest1
	{
		//People
		Mock<IPeopleService> moq;
		Person person = new Person
		{
			ID = 1
		};

		List<Person> people = new List<Person>
		{
			new Person
			{
				ID = 1
			},
			new Person
			{
				ID = 1
			}
		};

		//Employee
		private readonly IEmployeeService employeeService;
		Employee employee = new Employee { ID = 1 };

		public UnitTest1()
		{
			moq = new Mock<IPeopleService>(MockBehavior.Strict);
			this.employeeService = new EmployeeService(moq.Object);
		}

		[Fact]
		public void AddEmployee_Test()
		{
			//Arrange
			moq.Setup(p => p.AddPerson(person)).Returns(() => person);

			//Act
			bool isAdded = this.employeeService.AddEmployee(person);

			//Assert
			moq.Verify(p => p.AddPerson(person), Times.Exactly(1));
			Assert.True(isAdded);
		}

		[Fact]
		public void AddEmployee_Real_Test()
		{
			//Arrange
			var service = new EmployeeService(new PeopleService());

			//Act
			bool isAdded = service.AddEmployee(person);

			//Assert
			Assert.True(isAdded);
		}

		[Fact]
		public void AddEmployee_Exception_Test()
		{
			Person p1 = new Person { ID = 99999999 };
			//Arrange
			moq.Setup(p => p.AddPerson(p1)).Throws(new InvalidOperationException());

			//Act

			//Assert
			Assert.Throws<InvalidOperationException>(() => this.employeeService.AddEmployee(p1));
			moq.Verify(p => p.AddPerson(p1), Times.Exactly(1));
		}

		[Fact]
		public void GetEmployeeList_Test()
		{
			//Arrange
			moq.Setup(p => p.GetPeople()).Returns(people);

			//Act
			var employees = this.employeeService.GetEmployees();

			//Assert
			moq.Verify(p => p.GetPeople(), Times.Exactly(1));
			Assert.Equal(employees.Count, people.Count);
		}
	}
}
