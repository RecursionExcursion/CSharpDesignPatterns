using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Behavioral.Iterator
{
    /*
     * 
     */

    public class Person
    {
        public String Name { get; set; }
        public string Country { get; set; }

        public Person(string name, string country)
        {
            Name = name;
            Country = country;
        }
    }

    /*
     * Iterator
     */
    public interface IPeopleIterator
    {
        Person First();
        Person Next();
        bool IsDone { get; }
        Person CurrentItem { get; }
    }

    /*
     * Aggregate
     */
    public interface IPeopleCollection
    {
        IPeopleIterator CreateIterator();
    }

    /*
     * ConcreteAggregate
     */
    public class PeopleCollection : List<Person>, IPeopleCollection
    {
        public IPeopleIterator CreateIterator() => new PeopleIterator(this);
    }

    /*
     * ConcreteIterator
     */
    public class PeopleIterator : IPeopleIterator
    {
        private PeopleCollection _peopleCollection;
        private int _current = 0;

        public PeopleIterator(PeopleCollection peopleCollection)
        {
            _peopleCollection = peopleCollection;
        }

        public bool IsDone => _current >= _peopleCollection.Count;

        public Person CurrentItem => _peopleCollection[_current];

        public Person First() => _peopleCollection.OrderBy(p => p.Name).First();

        public Person Next()
        {
            _current++;
            return !IsDone ? _peopleCollection.OrderBy(p => p.Name).ToList()[_current] : null!;
        }
    }
}