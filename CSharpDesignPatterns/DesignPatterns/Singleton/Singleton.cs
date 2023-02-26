using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Singleton
{

    /*
     * Creational Pattern
     * Singleton
     */

    internal class Logger
    {

        
        private static Logger? _instance;

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
        }

        protected Logger()
        {
        }

        /*
         * SingletonOperation
         */

        public void Log(string message)
        {
            Console.WriteLine($"Message to log: {message}");
        }


    }

    /*
     * Thread Safe Singleton implementation
     */

    internal class ThreadSafeLogger
    {

        private static readonly Lazy<ThreadSafeLogger> _lazyLogger = new Lazy<ThreadSafeLogger>(() => new ThreadSafeLogger());

        protected ThreadSafeLogger()
        {

        }

        public static ThreadSafeLogger Instance
        {
            get
            {
                return _lazyLogger.Value;
            }
        }

        /*
       * SingletonOperation
       */

        public void Log(string message)
        {
            Console.WriteLine($"Message to log: {message}");
        }

    }
}
