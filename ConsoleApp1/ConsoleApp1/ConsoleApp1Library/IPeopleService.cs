using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1Library
{
	public interface IPeopleService
	{
		Person AddPerson(Person person);
		Person GetPerson(int id);
		List<Person> GetPeople();
	}

	public class Person
	{
		public int ID { get; set; }
	}
}
