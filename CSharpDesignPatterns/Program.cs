// See https://aka.ms/new-console-template for more information
using CSharpDesignPatterns.DesignPatterns.CreationalPatterns.Singleton;
using CSharpDesignPatterns.DesignPatterns.CreationalPatterns.Factory;
using CSharpDesignPatterns.DesignPatterns.CreationalPatterns.AbstractFactory;
using CSharpDesignPatterns.DesignPatterns.CreationalPatterns.Builder;
using CSharpDesignPatterns.DesignPatterns.Creational.Prototype;
using CSharpDesignPatterns.DesignPatterns.Structural.Adaptor;
using CSharpDesignPatterns.DesignPatterns.Structural.Bridge;
using CSharpDesignPatterns.DesignPatterns.Structural.Decorator;
using CSharpDesignPatterns.DesignPatterns.Structural.Composite;
using CSharpDesignPatterns.DesignPatterns.Structural.Facade;
using CSharpDesignPatterns.DesignPatterns.Structural.Proxy;
using CSharpDesignPatterns.DesignPatterns.Structural.FlyWeight;
using CSharpDesignPatterns.DesignPatterns.Behavioral.Template;
using CSharpDesignPatterns.DesignPatterns.Behavioral.Strategy;
using CSharpDesignPatterns.DesignPatterns.Behavioral.Command;
using CSharpDesignPatterns.DesignPatterns.Behavioral.Memento;
using CommandManager = CSharpDesignPatterns.DesignPatterns.Behavioral.Command.CommandManager;
using IEmployeeManagerRepository = CSharpDesignPatterns.DesignPatterns.Behavioral.Command.IEmployeeManagerRepository;
using EmployeeMamangerRepository = CSharpDesignPatterns.DesignPatterns.Behavioral.Command.EmployeeMamangerRepository;
using CSharpDesignPatterns.DesignPatterns.Behavioral.Mediator;
using CSharpDesignPatterns.DesignPatterns.Behavioral.ChainOfRespnsibility;
using System.ComponentModel.DataAnnotations;
using CSharpDesignPatterns.DesignPatterns.Behavioral.Observer;
using CSharpDesignPatterns.DesignPatterns.Behavioral.State;

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Singleton Pattern");

var test = Logger.Instance;
var test1 = Logger.Instance;


var test2 = ThreadSafeLogger.Instance;
var test3 = ThreadSafeLogger.Instance;

Console.WriteLine(test == test1);
Console.WriteLine(test2 == test3);

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\nFactory Pattern");

var factories = new List<DiscountFacory>
{
    new CodeDiscountFacory(Guid.NewGuid()),
    new CountryDiscountFacory("BE")
};

foreach (var factory in factories)
{
    var discountService = factory.CreateDiscountService();
    Console.WriteLine($"Percentage {discountService.DiscountPercentage} coming from {discountService}");
}

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("\nAbstract Factory");

var belgiumShoppingCartFactory = new BelgiumShoppingCartFactory();
var shoppingCartForBelgium = new ShoppingCart(belgiumShoppingCartFactory);
shoppingCartForBelgium.CalculateCosts();

var franceShoppingCartFactory = new FranceShoppingCartFactory();
var shoppingCartForFrance = new ShoppingCart(franceShoppingCartFactory);
shoppingCartForFrance.CalculateCosts();

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nBuilder Pattern");

var garage = new Garage();

var miniBuilder = new MiniBuilder();
var bmwBuilder = new BMWBuilder();

garage.Construct(miniBuilder);
garage.Show();

garage.Construct(bmwBuilder);
garage.Show();

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\nPrototype Pattern");

var manager = new CSharpDesignPatterns.DesignPatterns.Creational.Prototype.Manager("Cindy");
var managerClone = (CSharpDesignPatterns.DesignPatterns.Creational.Prototype.Manager) manager.Clone(true);
Console.WriteLine($"Manager was cloned: {managerClone.Name}");


var employee = new CSharpDesignPatterns.DesignPatterns.Creational.Prototype.Employee("Kevin", managerClone);
var employeeClone = (CSharpDesignPatterns.DesignPatterns.Creational.Prototype.Employee) employee.Clone(true);
Console.WriteLine($"Employee was cloned: {employeeClone.Name}, with manager {employeeClone.Manager.Name}");


managerClone.Name = "Court";
Console.WriteLine($"Employee was cloned: {employeeClone.Name}, with manager {employeeClone.Manager.Name}");

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.WriteLine("\n(Object) Adaptor Pattern");

ICityAdaptor adaptor = new CityAdaptor();
var city = adaptor.GetCity();

Console.WriteLine($"{city.FullName}, {city.Inhabitants}");

Console.WriteLine("\n(Class) Adaptor Pattern");

IClassCityAdaptor classAdaptor = new ClassCityAdaptor();
var classCity = classAdaptor.GetCity();

Console.WriteLine($"{classCity.FullName}, {classCity.Inhabitants}");

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("\nBridge Pattern");

NoCoupon noCoupon = new();
OneDollarCoupon oneDollarCoupon = new();

MeatMenu meatMenu = new(noCoupon);
Console.WriteLine($"Meat based menu, no coupon: ${meatMenu.CalculatePrice()}.");

meatMenu = new(oneDollarCoupon);
Console.WriteLine($"Meat based menu, one dollar coupon: ${meatMenu.CalculatePrice()}.");

VegitarianMenu vegMenu = new(noCoupon);
Console.WriteLine($"Meat based menu, no coupon: ${vegMenu.CalculatePrice()}.");

vegMenu = new(oneDollarCoupon);
Console.WriteLine($"Meat based menu, one dollar coupon: ${vegMenu.CalculatePrice()}.");

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine("\nDecorator Pattern");

CloudMailService cloudMailService = new();
cloudMailService.SendMail("Hi there");

OnPremiseMailService onPremiseMailService = new();
onPremiseMailService.SendMail("Hi there");

//Add behavior (decorate)
StatisticsDecorator statisticsDecorator = new(cloudMailService);
statisticsDecorator.SendMail($"Hi there via {nameof(StatisticsDecorator)} wrapper.");

MessageDatabaseDecorator messageDataBaseDecorator = new(onPremiseMailService);
messageDataBaseDecorator.SendMail($"Hi there via {nameof(MessageDatabaseDecorator)} wrapper, message 1.");
messageDataBaseDecorator.SendMail($"Hi there via {nameof(MessageDatabaseDecorator)} wrapper, message 2.");

foreach (var message in messageDataBaseDecorator.SentMessages)
{
    Console.WriteLine($"Stored message: \"{message} \"");
}


/*
 * 
 */
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("\nComposite Pattern");

CSharpDesignPatterns.DesignPatterns.Structural.Composite.Directory root = new("root", 0);
CSharpDesignPatterns.DesignPatterns.Structural.Composite.Directory topLevelFile = new("topLevel.txt", 100);
CSharpDesignPatterns.DesignPatterns.Structural.Composite.Directory topLevelDir1 = new("topLevel.txt", 4);
CSharpDesignPatterns.DesignPatterns.Structural.Composite.Directory topLevelDir2 = new("topLevel.txt", 4);

root.Add(topLevelFile);
root.Add(topLevelDir1);
root.Add(topLevelDir2);

CSharpDesignPatterns.DesignPatterns.Structural.Composite.Directory subLevelDir1 = new("topLevel.txt", 200);
CSharpDesignPatterns.DesignPatterns.Structural.Composite.Directory subLevelDir2 = new("topLevel.txt", 150);

topLevelDir2.Add(subLevelDir1);
topLevelDir2.Add(subLevelDir2);

Console.WriteLine($"Size of topLevelDir1: {topLevelDir1.GetSize()}");
Console.WriteLine($"Size of topLevelDir2: {topLevelDir2.GetSize()}");
Console.WriteLine($"Size of root: {root.GetSize()}");

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\nFacade Pattern");

DiscountFacade facade = new();

int x = 1;
int y = 10;
Console.WriteLine($"Discount % for customer with id {x}: {facade.CalculateDiscountPercentage(x)}");
Console.WriteLine($"Discount % for customer with id {y}: {facade.CalculateDiscountPercentage(y)}");

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\nProxy Pattern");

//Without Proxy
Console.WriteLine("Constructing document");
CSharpDesignPatterns.DesignPatterns.Structural.Proxy.Document doc = new("MyDoc.pdf");
Console.WriteLine("Doc constructed");
doc.DisplayDocument();

//with Proxy
Console.WriteLine("\nConstructing document");
DocumentProxy proxyDoc = new("MyDoc.pdf");
Console.WriteLine("Doc proxy constructed");
proxyDoc.DisplayDocument();

//with chained Proxies
Console.WriteLine("\nConstructing document");
ProtectedDocumentProxy protectedPoxyDoc = new("MyDoc.pdf", "Viewer");
Console.WriteLine("Doc proxy constructed");
protectedPoxyDoc.DisplayDocument();

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\nFlyweight Pattern");

var someChars = "abba";

CharacterFactory characterFactory = new();

var charObj = characterFactory.GetCharacter(someChars[0]);

foreach (char c in someChars)
{
    characterFactory.GetCharacter(c)?.Draw("Arial", 12);
}

//Unshared Conrcrete Flyweight
var paragraph = characterFactory.CreateParagraph(new List<ICharacter>() { charObj }, 1);

paragraph.Draw("Times New Roman", 8);


/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\nTemplate Pattern");

ExchangeMailParser exchangeMailParser = new();
Console.WriteLine(exchangeMailParser.ParseMailBody("bdi723e-89883-d34f34-dcd2d23d23d"));
Console.WriteLine();

ApacheMailParser apacheMailParser = new();
Console.WriteLine(apacheMailParser.ParseMailBody("sdfsf-89834f43f83-3f4f34-45g45g45g4"));
Console.WriteLine();


EudoraMailParser eurdoraMailParser = new();
Console.WriteLine(eurdoraMailParser.ParseMailBody("4g4g45g4-gdrgdrg-4g45-g45geg45"));


/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("\nStrategy Pattern");

Order order = new("Marvin Software", 5, "Visual Studio License");

order.Export(new CSVExportService());
order.Export(new JsonExportService());
order.Export(new XMLExportService());

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.DarkGray;
Console.WriteLine("\nCommand Pattern");

CommandManager commandManager = new();
IEmployeeManagerRepository repository = new EmployeeMamangerRepository();

commandManager.Invoke(new CSharpDesignPatterns.DesignPatterns.Behavioral.Command.AddEmployeeToManagerList(repository, 1,
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Command.Employee(111, "Kevin")));
repository.WriteDataStore();

commandManager.Invoke(new CSharpDesignPatterns.DesignPatterns.Behavioral.Command.AddEmployeeToManagerList(repository, 1,
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Command.Employee(222, "Mark")));
repository.WriteDataStore();

commandManager.Invoke(new CSharpDesignPatterns.DesignPatterns.Behavioral.Command.AddEmployeeToManagerList(repository, 2,
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Command.Employee(333, "Foofers")));
repository.WriteDataStore();

//Adding duplicate employee
commandManager.Invoke(new CSharpDesignPatterns.DesignPatterns.Behavioral.Command.AddEmployeeToManagerList(repository, 2,
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Command.Employee(333, "Foofers")));
repository.WriteDataStore();

commandManager.Undo();
repository.WriteDataStore();

commandManager.UndoAll();
repository.WriteDataStore();

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\nMemento Pattern");

CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.CommandManager mementoCommandManager = new();
CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.IEmployeeManagerRepository mementoRepository =
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.EmployeeMamangerRepository();

mementoCommandManager.Invoke(
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.AddEmployeeToManagerList(mementoRepository, 1,
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.Employee(111, "Kevin")));

mementoRepository.WriteDataStore();

mementoCommandManager.Invoke(
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.AddEmployeeToManagerList(mementoRepository, 1,
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.Employee(222, "Mark")));

mementoRepository.WriteDataStore();

mementoCommandManager.Invoke(
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.AddEmployeeToManagerList(mementoRepository, 2,
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.Employee(333, "Foofers")));

mementoRepository.WriteDataStore();

//Adding duplicate employee
mementoCommandManager.Invoke(
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.AddEmployeeToManagerList(mementoRepository, 2,
    new CSharpDesignPatterns.DesignPatterns.Behavioral.Memento.Employee(333, "Foofers")));

mementoRepository.WriteDataStore();

mementoCommandManager.Undo();
mementoRepository.WriteDataStore();

mementoCommandManager.UndoAll();
mementoRepository.WriteDataStore();


/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\nMediator Pattern");

TeamChatRoom teamChatRoom = new();

Lawyer foof = new("Foofers");
Lawyer court = new("Court");
AccountManager ryan = new("Ryan");
AccountManager bob = new("Bob");
AccountManager don = new("Don");

teamChatRoom.Register(bob, foof, court, ryan, don);

bob.Send("Happy Wednesday everyone!");
foof.Send("Woof!");
ryan.Send(court.Name, "I love you");
court.SendTo<Lawyer>("He loves me!");

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\nChain of Responsibility Pattern");

CSharpDesignPatterns.DesignPatterns.Behavioral.ChainOfRespnsibility.Document validDocument = new("Java is way better that C#", DateTimeOffset.UtcNow, true, true);
CSharpDesignPatterns.DesignPatterns.Behavioral.ChainOfRespnsibility.Document invalidDocument = new("Java is way worse that C#", DateTimeOffset.UtcNow, false, true);

var documentHandlerChain = new DocumentTitleHandler();
documentHandlerChain
    .SetSuccessor(new DocumentLastModifiedHandler())
    .SetSuccessor(new DocumentApprovedByLitigationHandler())
    .SetSuccessor(new DocumentApprovedByManagementHandler());

try
{
    documentHandlerChain.Handle(validDocument);
    Console.WriteLine("Valid doc is valid");
    documentHandlerChain.Handle(invalidDocument);
    Console.WriteLine("Invalid doc is valid");

} catch (ValidationException validationException)
{
    Console.Write("Exception thrown-\t");
    Console.WriteLine(validationException.Message);
}


/*
 * 
 */
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nObserver Pattern");

TicketStockService ticketStockService = new();
TicketResellerService ticketResellerService = new();
CSharpDesignPatterns.DesignPatterns.Behavioral.Observer.OrderService orderService = new();

orderService.AddObserver(ticketStockService);
orderService.AddObserver(ticketResellerService);

orderService.CompleteTicketSale(1, 2);

Console.WriteLine();

orderService.RemoveObserver(ticketStockService);

orderService.CompleteTicketSale(2, 4);

/*
 * 
 */
Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("\nState Pattern");

BankAccount bankAccount = new();
bankAccount.Deposit(100);
bankAccount.Withdraw(500);      
bankAccount.Withdraw(100);

bankAccount.Deposit(1500);
bankAccount.Deposit(100);
bankAccount.Deposit(100);
bankAccount.Withdraw(2000);
