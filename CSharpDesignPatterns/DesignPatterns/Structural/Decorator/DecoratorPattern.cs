using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Structural.Decorator
{

    /*
     * AKA Wrapper
     * Adds responsiblities to an object
     */

    /*
     * Component (as interface)
     */

    public interface IMailService
    {
        bool SendMail(string message);
    }

    /*
     * ConcreteComponents
     */

    public class CloudMailService : IMailService
    {
        public bool SendMail(string message)
        {
            Console.WriteLine($"Message \"{message}\" sent via {nameof(CloudMailService)}.");
            return true;
        }
    }
    public class OnPremiseMailService : IMailService
    {
        public bool SendMail(string message)
        {
            Console.WriteLine($"Message \"{message}\" sent via {nameof(OnPremiseMailService)}.");
            return true;
        }
    }

    /*
     * Decorator
     */

    public abstract class MailServiceDecoratorBase : IMailService
    {

        private readonly IMailService _mailService;

        public MailServiceDecoratorBase(IMailService mailService)
        {
            _mailService = mailService;
        }

        public virtual bool SendMail(string message)
        {
            return _mailService.SendMail(message);
        }
    }

    /*
     * ConcreteDecorator
     */

    public class StatisticsDecorator : MailServiceDecoratorBase
    {
        public StatisticsDecorator(IMailService mailService) : base(mailService)
        {
        }

        public override bool SendMail(string message)
        {
            //Sham Stats collector
            Console.WriteLine($"Collecting statistics in {nameof(StatisticsDecorator)}.");
            return base.SendMail(message);
        }
    }

    public class MessageDatabaseDecorator : MailServiceDecoratorBase
    {

        public List<string> SentMessages { get; set; } = new List<string>();

        public MessageDatabaseDecorator(IMailService mailService) : base(mailService)
        {
        }

        public override bool SendMail(string message)
        {
            if (base.SendMail(message))
            {
                //Store Message Sent
                SentMessages.Add(message);
                return true;
            }
            return false;
        }
    }
}
