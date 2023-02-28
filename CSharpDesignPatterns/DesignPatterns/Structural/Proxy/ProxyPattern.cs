using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Structural.Proxy
{


    /*
     *AKA SurrogatePattern 
     *
     *RemoteProxy-  Provide local representaion of a an object in a differnet network
     *VirtualProxy- Creates expensive objects on demand
     *SmartProxy- Caching and locking
     *ProtectionProxy- Control access
     */

    /*
     * Subject
     */
    public interface IDocument
    {
        void DisplayDocument();
    }


    /*
     * RealSubject
     */
    public class Document : IDocument
    {
        public string? Title { get; private set; }
        public string? Content { get; private set; }
        public int AuthorId { get; private set; }
        public DateTime LastAccessed { get; private set; }
        private string? _fileName;

        public Document(string? fileName)
        {
            _fileName = fileName;
            LoadDocument(fileName);
        }

        private void LoadDocument(string? fileName)
        {
            Console.WriteLine("Executing expensive action: loading a file from disk");
            //Fake Loading
            Title = "An expensive document";
            Content = "Alot of stuff";
            AuthorId = 1;
            LastAccessed = DateTime.UtcNow;
        }

        public void DisplayDocument()
        {
            Console.WriteLine($"Title: {Title}, Content: {Content}");
        }
    }

    /*
     * Proxy
     */
    public class DocumentProxy : IDocument
    {
        /*
        private Document? _document;
        Defer to Lazy Initalization
        Lazy<T> is thread safe
        */
        private Lazy<Document> _document;
        private string _fileName;

        public DocumentProxy(string fileName)
        {
            _fileName = fileName;
            _document = new Lazy<Document>(() => new Document(_fileName));
        }

        public void DisplayDocument()
        {
            /*
            if (_document == null)
            {
      
                 _document = new(_fileName);
                
            }
            _document.DisplayDocument();
            */
            _document.Value.DisplayDocument();
        }
    }

    public class ProtectedDocumentProxy : IDocument
    {
        private string _fileName;
        private string _userRole;
        //Using Composition to chain proxies
        private DocumentProxy _documentProxy;

        public ProtectedDocumentProxy(string fileName, string userRole)
        {
            _fileName = fileName;
            _userRole = userRole;
            _documentProxy = new DocumentProxy(fileName);
        }

        public void DisplayDocument()
        {
            Console.WriteLine($"Entering DisplayDocument in {nameof(ProtectedDocumentProxy)}");

            if(_userRole != "Viewer") { throw new UnauthorizedAccessException(); }

            _documentProxy.DisplayDocument();

            Console.WriteLine($"Exiting DisplayDocument in {nameof(ProtectedDocumentProxy)}");

        }
    }
}
