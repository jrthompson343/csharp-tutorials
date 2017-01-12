using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals.Examples.LINQ
{
    public class CustomLinq : IExampleRunner
    {
        public void RunExample()
        {
            PersonRepository repo = new PersonRepository();
            IEnumerable <Person> peopleWhoStartWithB = repo.GetAll().Where(p => p.Name.StartsWith("B"));
            foreach (var person in peopleWhoStartWithB)
            {
                Console.WriteLine(person.ToString());
            }
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }

    public class PersonRepository
    {
        private IEnumerable<Person> _people; 
        public PersonRepository()
        {
            _people = new List<Person>
            {
                new Person {Age = 65, Name = "Smith"},
                new Person {Age = 87, Name = "Bort"},
                new Person {Age = 98, Name = "Jort"}
            };
        }

        public IEnumerable<Person> GetAll()
        {
            return _people;
        } 
    }

    public static class PersonExtensions
    {
        public static IEnumerable<Person> Where(this IEnumerable<Person> sequence, Func<Person, bool> predicate)
        {
            foreach (var p in sequence)
            {
                if (predicate(p))
                {
                    Console.WriteLine("Called");
                    yield return p;
                }
            }
        }
    }
}
