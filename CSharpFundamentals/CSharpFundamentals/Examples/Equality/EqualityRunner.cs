using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals.Examples.Equality
{
    public class EqualityRunner : IExampleRunner
    {
        public void RunExample()
        {
            Animal aGeorge = new Animal("George");
            Animal bGeorge = new Animal("George");
            Cat aMeowsor = new Cat("Meowsor", 25);
            Cat bMeowser = new Cat("Meowsor", 26);

            Console.WriteLine(AreAnimalsEqual(aGeorge, bGeorge));
            Console.WriteLine(AreAnimalsEqual(aMeowsor, bMeowser));
            Console.ReadLine();
        }

        private bool AreAnimalsEqual(Animal x, Animal y)
        {
            return x == y;
        }
    }
    public class Animal
    {
        private readonly string _name;
        public string Name { get { return _name; } }
        public Animal(string name)
        {
            _name = name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Animal animal = obj as Animal;
            return animal.Name == this.Name;
        }

        public static bool operator ==(Animal x, Animal y)
        {
            return x.Name == y.Name;
        }

        public static bool operator !=(Animal x, Animal y)
        {
            return x.Name != y.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }

    public class Cat : Animal
    {
        private readonly int _numWhiskers;
        public int NumWhiskers { get { return _numWhiskers; } }

        public Cat(string name, int whiskers) : base(name)
        {
            _numWhiskers = whiskers;
        }

        public static bool operator ==(Cat x, Cat y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(Cat x, Cat y)
        {
            return !object.Equals(x, y);
        }
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            Cat cat = (Cat)obj;
            return cat.NumWhiskers == this.NumWhiskers;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ NumWhiskers;
        }
    }
    
}
