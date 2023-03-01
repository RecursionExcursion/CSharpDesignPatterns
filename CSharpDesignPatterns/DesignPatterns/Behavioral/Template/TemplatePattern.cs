using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Behavioral.Template
{

    /*
     *Most used Design Pattern 
     */


    /*
     * AbstractClass
     */
    public abstract class MailParser
    {
        //'virtual allows method to be overridden in C#'
        protected virtual void FindServer()
        {
            Console.WriteLine("Finding Server....");
        }

        protected abstract void AuthenticateToServer();

        protected string ParseHtmlMailBody(string identifier)
        {
            Console.WriteLine("Parsing HTML mail body...");
            return $"This is the body of mail with id {identifier}";
        }

        public string ParseMailBody(string identifier)
        {
            Console.WriteLine("Parsing mail body (in template method)");
            FindServer();
            AuthenticateToServer();
            return ParseHtmlMailBody(identifier);
        }
    }

    /*
     * ConcreteImplementationOfAbstractClass
     */
    public class ExchangeMailParser : MailParser
    {
        protected override void AuthenticateToServer()
        {
            Console.WriteLine("Connecting to Exchange");
        }
    }

    public class ApacheMailParser : MailParser
    {
        protected override void AuthenticateToServer()
        {
            Console.WriteLine("Connecting to Apache");
        }
    }

    public class EudoraMailParser : MailParser
    {
        protected override void AuthenticateToServer()
        {
            Console.WriteLine("Connecting to Eudora");
        }
        protected override void FindServer()
        {
            Console.WriteLine("Finding Eudora through a custom algorithm...");
        }
    }
}
