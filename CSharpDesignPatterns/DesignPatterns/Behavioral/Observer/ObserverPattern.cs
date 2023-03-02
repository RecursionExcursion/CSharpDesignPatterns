using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Behavioral.Observer
{

    /*
     * Define a one to many dependency
     * Maintains synchronization
     * AKA pub/sub pattern
     */


    /*
     * 
     */
    public class TicketChange
    {
        public int Amount { get; private set; }
        public int ArtistId { get; private set; }

        public TicketChange(int artistId, int amount)
        {
            Amount = amount;
            ArtistId = artistId;
        }
    }

    /*
     * Subject
     */
    public abstract class TicketChangeNotifier
    {
        private List<ITicketChangeListener> _observers = new();

        public void AddObserver(ITicketChangeListener observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(ITicketChangeListener observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(TicketChange ticketChange)
        {
            foreach (var observer in _observers)
            {
                observer.RecieveTicketChangeNotification(ticketChange);
            }
        }
    }

    /*
     * Observer
     */
    public interface ITicketChangeListener
    {
        void RecieveTicketChangeNotification(TicketChange ticketChange);
    }

    /*
    * ConcreteSubject
    */
    public class OrderService : TicketChangeNotifier
    {
        public void CompleteTicketSale(int artistId, int amount)
        {
            //Change local datastore. DS omitted for demo
            Console.WriteLine($"{nameof(OrderService)} is changing its state.");
            Console.WriteLine($"{nameof(OrderService)} is notifiying observers...");
            Notify(new TicketChange(artistId, amount));
        }
    }

    /*
     * ConcreteObserver
     */
    public class TicketResellerService : ITicketChangeListener
    {
        public void RecieveTicketChangeNotification(TicketChange ticketChange)
        {
            //Change local datastore. DS omitted for demo
            Console.WriteLine($"{nameof(TicketResellerService)} notified of ticket change: artist {ticketChange.ArtistId}," +
                $" amount {ticketChange.Amount}.");
        }
    }
    public class TicketStockService : ITicketChangeListener
    {
        public void RecieveTicketChangeNotification(TicketChange ticketChange)
        {
            //Change local datastore. DS omitted for demo
            Console.WriteLine($"{nameof(TicketStockService)} notified of ticket change: artist {ticketChange.ArtistId}," +
                $" amount {ticketChange.Amount}.");
        }
    }
}


