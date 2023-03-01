using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Behavioral.Strategy
{

    /*
     * AKA Policy Pattern 
     * One of the most common used patterns
     * 
     * Use when
     * Many related classes differ only in thier behavior
     * When you need different variants of an algo which you want to be able to change at runtime
     * when a lgo uses data, code, or dependencies that the clients shouldn't know about
     * When a class has many different behaviors which appear as a bunch of conditional statementsS
     */


    /*
     * Strategy
     */
    public interface IExportService
    {
        void Export(Order order);
    }

    /*
     * ConcreteStrategies
     */
    public class JsonExportService : IExportService
    {
        public void Export(Order order)
        {
            Console.WriteLine($"Exporting {order.Name} to Json.");
        }
    }
    public class XMLExportService : IExportService
    {
        public void Export(Order order)
        {
            Console.WriteLine($"Exporting {order.Name} to XML.");
        }
    }
    public class CSVExportService : IExportService
    {
        public void Export(Order order)
        {
            Console.WriteLine($"Exporting {order.Name} to CSV.");
        }
    }

    /*
     * Context
     */
    public class Order
    {
        private string Customer { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public string? Descprtion { get; set; }

        //public IExportService? ExportService { get; set; }

        public Order(string customer, int amount, string name)
        {
            Customer = customer;
            Amount = amount;
            Name = name;
        }

        public void Export(IExportService exportService)
        {
            if(exportService is null)
            {
                throw new ArgumentNullException(nameof(exportService));
            }
            exportService.Export(this);
        }
    }
}
