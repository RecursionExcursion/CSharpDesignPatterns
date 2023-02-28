using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Creational.Prototype
{

    /*
     *Prototype 
     */
    public abstract class Person
    {
        public abstract string Name
        {
            get; set;
        }
        public abstract Person Clone(bool deepClone);
    }

    /*
     * ConcretePrototypes
     */

    public class Manager : Person
    {
        public override string Name { get; set; }
        public Manager(string name)
        {
            Name = name;
        }
        public override Person Clone(bool deepClone = false)
        {
            if (deepClone)
            {
                var objectAsJson = JsonConvert.SerializeObject(this);
                return JsonConvert.DeserializeObject<Manager>(objectAsJson);
            } else
            {
                return (Person) MemberwiseClone();
            }
        }
    }
    public class Employee : Person
    {
        public Manager Manager { get; set; }
        public override string Name { get; set; }
        public Employee(string name, Manager manager)
        {
            Manager = manager;
            Name = name;
        }
        public override Person Clone(bool deepClone = false)
        {
            if (deepClone)
            {
                var objectAsJson = JsonConvert.SerializeObject(this);
                return JsonConvert.DeserializeObject<Employee>(objectAsJson);
            } else
            {
                return (Person) MemberwiseClone();
            }
        }
    }
}
