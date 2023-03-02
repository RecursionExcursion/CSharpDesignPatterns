using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Behavioral.Visitor
{

    /*
     * Represent an operation to be peroformed on the elements of the an object structure
     */

    /*
     * ConcreteElements
     */
    public class Customer : IElement
    {
        public string Name { get; private set; }
        public decimal AmountOrdered { get; private set; }
        public decimal Discount { get; set; }

        public Customer(string name, decimal amountOrdered)
        {
            Name = name;
            AmountOrdered = amountOrdered;
        }

        public void Accept(IVisitor visitor)
        {
            //visitor.VisitCustomer(this);
            visitor.Visit(this);
            Console.WriteLine($"Visited {nameof(Customer)} {Name}, discount given: {Discount}");
        }
    }
    public class Employee : IElement
    {
        public string Name { get; private set; }
        public int YearsEmployeed { get; private set; }
        public decimal Discount { get; set; }

        public Employee(string name, int yearsEmployeed)
        {
            Name = name;
            YearsEmployeed = yearsEmployeed;
        }

        public void Accept(IVisitor visitor)
        {
            //visitor.VisitEmployee(this);
            visitor.Visit(this);
            Console.WriteLine($"Visited {nameof(Employee)} {Name}, discount given: {Discount}");
        }
    }

    /*
     * Visitor
     *
    public interface IVisitor
    {
        void VisitCustomer(Customer customer);
        void VisitEmployee(Employee employee);
    }
    */

    /*
     * Visitor (alternative)
     */
    public interface IVisitor
    {
        void Visit(IElement element);
    }

    /*
     * Element
     */
    public interface IElement
    {
        void Accept(IVisitor visitor);
    }

    /*
     * ConcreteVisitor
     */
    public class DiscountVisitor : IVisitor
    {
        public decimal TotalDiscountsGiven { get; set; }

        public void Visit(IElement element)
        {
            if (element is Customer)
            {
                VisitCustomer((Customer) element);
            } else if (element is Employee)
            {
                VisitEmployee((Employee) element);
            }
        }
        private void VisitCustomer(Customer customer)
        {
            decimal discount = customer.AmountOrdered / 10;
            customer.Discount = discount;
            AddToTotalDiscounts(discount);
        }
        private void VisitEmployee(Employee employee)
        {
            int discount = employee.YearsEmployeed < 10 ? 100 : 200;
            employee.Discount = discount;
            AddToTotalDiscounts(discount);
        }
        private void AddToTotalDiscounts(decimal discount) => TotalDiscountsGiven += discount;
    }

    /*
     * ObjectStructure
     */
    public class Container
    {
        public List<Employee> Employees { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();

        public void Accept(IVisitor visitor)
        {
            foreach (Employee employee in Employees)
            {
                employee.Accept(visitor);
            }
            foreach (Customer customer in Customers)
            {
                customer.Accept(visitor);
            }
        }
    }
}
