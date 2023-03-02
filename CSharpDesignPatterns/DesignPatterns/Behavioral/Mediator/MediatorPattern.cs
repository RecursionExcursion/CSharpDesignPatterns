using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Behavioral.Mediator
{
    /*
     * Encapsulated how a set of objects interact
     * Loosly couples these objects
     */


    /*
     * Mediator
     */
    public interface IChatRoom
    {
        public abstract void Register(params TeamMember[] teamMember);
        public abstract void Send(string from, string message);
        public abstract void Send(string from, string to, string message);
        public abstract void SendTo<T>(string from, string message) where T : TeamMember;
    }

    /*
     * Colleague
     */
    public abstract class TeamMember
    {
        private IChatRoom? _chatRoom;
        public string Name { get; set; }

        protected TeamMember(string name)
        {
            Name = name;
        }

        internal void SetChatroom(IChatRoom chatRoom)
        {
            _chatRoom = chatRoom;
        }

        public void SendTo<T>(string message) where T : TeamMember
        {
            _chatRoom?.SendTo<T>(Name, message);
        }

        public void Send(string message)
        {
            _chatRoom?.Send(Name, message);
        }

        public void Send(string to, string message)
        {
            _chatRoom?.Send(Name, to, message);
        }

        public virtual void Receive(string from, string message)
        {
            Console.WriteLine($"Message {from} to {Name}: {message}");
        }
    }

    /*
     * ConcreteColleague
     */
    public class Lawyer : TeamMember
    {
        public Lawyer(string name) : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.WriteLine($"{nameof(Lawyer)} {Name} recieved: ");
            base.Receive(from, message);
        }
    }
    public class AccountManager : TeamMember
    {
        public AccountManager(string name) : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.WriteLine($"{nameof(AccountManager)} {Name} recieved: ");
            base.Receive(from, message);
        }
    }

    /*
     * ConcreteMediator
     */
    public class TeamChatRoom : IChatRoom
    {

        private readonly Dictionary<string, TeamMember> _teamMembers = new();
        public void Register(params TeamMember[] teamMembers)
        {
            foreach (var teamMember in teamMembers)
            {
                teamMember.SetChatroom(this);
                _teamMembers.TryAdd(teamMember.Name, teamMember);
            }
        }

        public void Send(string from, string message)
        {
            foreach (var teamMember in _teamMembers.Values)
            {
                teamMember.Receive(from, message);
            }
        }

        public void Send(string from, string to, string message)
        {
            _teamMembers[to]?.Receive(from, message);
        }

        public void SendTo<T>(string from, string message) where T : TeamMember
        {
            foreach(var member in _teamMembers.Values.OfType<T>())
            {
                member.Receive(from, message);
            }
        }
    }
}
