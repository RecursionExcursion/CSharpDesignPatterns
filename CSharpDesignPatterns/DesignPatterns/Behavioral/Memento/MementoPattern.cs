using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Behavioral.Memento
{

    /*
     * AKA TokenPattern
     * 
     * Capture and exeternalize and objects internal state
     */

    /*
     * Memento
     */
    public class AddEmployeeToManagerListMemento
    {
        public int ManagerId { get; private set; }
        public Employee? Employee { get; private set; }

        public AddEmployeeToManagerListMemento(int managerId, Employee? employee)
        {
            ManagerId = managerId;
            Employee = employee;
        }
    }

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
     * ConcreteCommand & Originator
     */
    public class AddEmployeeToManagerList : ICommand
    {

        private readonly IEmployeeManagerRepository _employeeManagerRepository;
        private int _managerId;
        private Employee? _employee;

        public AddEmployeeToManagerList(
            IEmployeeManagerRepository employeeManagerRepository,
            int managerId,
            Employee? employee)
        {
            _employeeManagerRepository = employeeManagerRepository;
            _managerId = managerId;
            _employee = employee;
        }

        public AddEmployeeToManagerListMemento CreateMemento()
        {
            return new AddEmployeeToManagerListMemento(_managerId, _employee);
        }

        public void RestoreMemento(AddEmployeeToManagerListMemento memento)
        {
            _managerId = memento.ManagerId;
            _employee = memento.Employee;
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
     * Invoker & Caretaker
     */
    public class CommandManager
    {

        private readonly Stack<AddEmployeeToManagerListMemento> _memento = new();
        private AddEmployeeToManagerList? _command;

        public void Invoke(ICommand command)
        {

            /* If the command has not been sotred yet, store it - we will reuse if instead of storing different instances  */
            if (_command == null)
            {
                _command = (AddEmployeeToManagerList) command;
            }

            if (command.CanExecute())
            {
                command.Execute();
                _memento.Push(((AddEmployeeToManagerList) command).CreateMemento());
            }
        }

        public void Undo()
        {
            if (_memento.Any())
            {
                _command?.RestoreMemento(_memento.Pop());
                _command?.Undo();
            }
        }
        public void UndoAll()
        {
            while (_memento.Any())
            {
                _command?.RestoreMemento(_memento.Pop());
                _command?.Undo();
            }
        }
    }
}
