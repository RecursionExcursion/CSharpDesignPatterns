using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Behavioral.Command
{

    /*
     * AKA Action or Transaction Pattern
     * 
     * Encapsulates requests as objects
     * Decouples requester from reciever
     * 
     * Use Cases
     * When you want to parameterize objects with an action
     * When you want to support Undo functionaliy
     * When you want to specify, queue and execute requests at differnt times
     * When you need to store a list og changes to potentially reapply later on
     * 
     */



    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Manager : Employee
    {
        public List<Employee> Employees = new();
        public Manager(int id, string name) : base(id, name) { }
    }

    /*
     * Reciever (interface)
     */
    public interface IEmployeeManagerRepository
    {
        void AddEmployee(int managerId, Employee employee);
        void RemoveEmployee(int managerId, Employee employee);
        bool HasEmployee(int managerId, int employeeId);
        void WriteDataStore();
    }

    /*
     * Reciever (implementation)
     */
    public class EmployeeMamangerRepository : IEmployeeManagerRepository
    {
        //For demo, in memory datastore
        private List<Manager> _managers = new List<Manager>() { new(1, "Kaite"), new(2, "Jeff") };

        public void AddEmployee(int managerId, Employee employee)
        {
            _managers.First(m => m.Id == managerId).Employees.Add(employee);
        }
        public void RemoveEmployee(int managerId, Employee employee)
        {
            _managers.First(m => m.Id == managerId).Employees.Remove(employee);
        }

        public bool HasEmployee(int managerId, int employeeId)
        {
            return _managers.First(m => m.Id == managerId).Employees.Any(e => e.Id == employeeId);

        }

        public void WriteDataStore()
        {
            foreach (var manager in _managers)
            {
                Console.WriteLine($"Manager {manager.Id}, {manager.Name}");
                if (manager.Employees.Any())
                {
                    foreach (var employee in manager.Employees)
                    {
                        Console.WriteLine($"\tEmployee {employee.Id}, {employee.Name}");
                    }
                } else
                {
                    Console.WriteLine($"\tNo employees");

                }
            }
        }
    }

    /*
     * Command
     */
    public interface ICommand
    {
        void Execute();
        bool CanExecute();
        void Undo();
    }

    /*
     * ConcreteCommand
     */
    public class AddEmployeeToManagerList : ICommand
    {

        private readonly IEmployeeManagerRepository _employeeManagerRepository;
        private readonly int _managerId;
        private readonly Employee? _employee;

        public AddEmployeeToManagerList(
            IEmployeeManagerRepository employeeManagerRepository,
            int managerId,
            Employee? employee)
        {
            _employeeManagerRepository = employeeManagerRepository;
            _managerId = managerId;
            _employee = employee;
        }

        public bool CanExecute()
        {
            //Check if employee is null or if employee already exists
            if (_employee is null || _employeeManagerRepository.HasEmployee(_managerId, _employee.Id))
            {
                return false;
            }
            return true;
        }

        public void Execute()
        {
            if (_employee != null)
            {
                _employeeManagerRepository.AddEmployee(_managerId, _employee);
            }
        }

        public void Undo()
        {
            if (_employee != null)
            {
                _employeeManagerRepository.RemoveEmployee(_managerId, _employee);
            }
        }
    }

    /*
     * Invoker
     */
    public class CommandManager
    {

        private readonly Stack<ICommand> _commands = new();
        public void Invoke(ICommand command)
        {
            if (command.CanExecute())
            {
                command.Execute();
                _commands.Push(command);
            }
        }

        public void Undo()
        {
            if (_commands.Any())
            {
                _commands.Pop()?.Undo();
            }
        }
        public void UndoAll()
        {
            while (_commands.Any())
            {
                _commands.Pop()?.Undo();
            }
        }
    }
}
