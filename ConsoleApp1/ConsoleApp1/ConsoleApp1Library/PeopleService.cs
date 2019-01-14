using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1Library
{
	public class PeopleService : IPeopleService
	{
		private List<Person> people { get; set; } = new List<Person>();

		public Person AddPerson(Person person)
		{
			people.Add(person);

			return person;
		}

		public Person GetPerson(int id)
		{
			return people?.FirstOrDefault(p => p.ID == id);
		}

		public List<Person> GetPeople()
		{
			return people;
		}
	}
}
