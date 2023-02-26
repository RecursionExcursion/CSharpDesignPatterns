// See https://aka.ms/new-console-template for more information
using CSharpDesignPatterns.DesignPatterns.Singleton;
using CSharpDesignPatterns.DesignPatterns.Factory;

Console.WriteLine("Singleton Pattern");

var test = Logger.Instance;
var test1 = Logger.Instance;


var test2 = ThreadSafeLogger.Instance;
var test3 = ThreadSafeLogger.Instance;

Console.WriteLine(test == test1);
Console.WriteLine(test2 == test3);

Console.WriteLine("Factory Pattern");

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

Console.ReadKey();

