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

var manager = new Manager("Cindy");
var managerClone = (Manager) manager.Clone(true);
Console.WriteLine($"Manager was cloned: {managerClone.Name}");


var employee = new Employee("Kevin", managerClone);
var employeeClone = (Employee) employee.Clone(true);
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

DiscountFacade facade= new();

int x = 1;
int y = 10;

Console.WriteLine($"Discount % for customer with id {x}: {facade.CalculateDiscountPercentage(x)}");
Console.WriteLine($"Discount % for customer with id {y}: {facade.CalculateDiscountPercentage(y)}");


Console.ResetColor();



