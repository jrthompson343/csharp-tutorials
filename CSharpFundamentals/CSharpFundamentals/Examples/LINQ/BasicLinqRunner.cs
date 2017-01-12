using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals.Examples.LINQ
{
    public class BasicLinqRunner : IExampleRunner
    {
        private readonly List<Person> _people;

        public BasicLinqRunner()
        {
            _people = new List<Person>
            {
                new Person {Name = "Bill", Age = 23},
                new Person {Name = "Jermey", Age = 16},
                new Person {Name = "Cleatus", Age = 56}
            };
        }
        public void RunExample()
        {
            IEnumerable<Person> oldFolks = FindPeopleOver(30);

            _people.Add(new Person { Name = "George", Age = 78});
            PrintEnumeration(oldFolks);
        }

        public IEnumerable<Person> FindPeopleOver(int age)
        {
            IEnumerable<Person> peopleOverThirty = from p in _people
                                                   where p.Age >= age
                                                   select p;
            return peopleOverThirty;
        }

        public static void PrintEnumeration(IEnumerable<Person> people)
        {
            foreach (var p in people)
            {
                Console.WriteLine(p.ToString());
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
    }
}
