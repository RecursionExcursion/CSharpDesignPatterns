using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.CreationalPatterns.AbstractFactory
{

    /*
     * AbstractFactory
     */
    public interface IShoppingCartPurchaseFactory
    {
        IDiscountService CreateDiscountSerivce();
        IShippingCostsServices CreateShippingCostsServices();
    }


    /*
     * AbstractProducts
     */

    public interface IDiscountService
    {
        int DiscountPercentage
        {
            get;
        }
    }

    public interface IShippingCostsServices
    {
        decimal ShippingCosts
        {
            get;
        }
    }

    /*
     * ConcreteProducts
     */

    public class BelgiumDiscountService : IDiscountService
    {
        public int DiscountPercentage => 20;
    }
    public class FranceDiscountService : IDiscountService
    {
        public int DiscountPercentage => 10;
    }
    public class BelgiumShippingCostService : IShippingCostsServices
    {
        public decimal ShippingCosts => 20;
    }
    public class FranceShippingCostService : IShippingCostsServices
    {
        public decimal ShippingCosts => 25;
    }

    /*
     * ConcreteFactory
     */

    public class BelgiumShoppingCartFactory : IShoppingCartPurchaseFactory
    {
        public IDiscountService CreateDiscountSerivce() => new BelgiumDiscountService();
        public IShippingCostsServices CreateShippingCostsServices() => new BelgiumShippingCostService();
    }
    public class FranceShoppingCartFactory : IShoppingCartPurchaseFactory
    {
        public IDiscountService CreateDiscountSerivce() => new FranceDiscountService();
        public IShippingCostsServices CreateShippingCostsServices() => new FranceShippingCostService();
    }

    /*
     * ClientClass
     */

    public class ShoppingCart
    {
        private readonly IDiscountService _discountSerivce;
        private readonly IShippingCostsServices _shippingCostsService;
        private int _orderCosts;

        public ShoppingCart(IShoppingCartPurchaseFactory factory)
        {
            _discountSerivce = factory.CreateDiscountSerivce();
            _shippingCostsService = factory.CreateShippingCostsServices();
            _orderCosts = 200;
        }

        public void CalculateCosts()
        {
            Console.WriteLine($"Total costs = {_orderCosts - _orderCosts / 100 * _discountSerivce.DiscountPercentage + _shippingCostsService.ShippingCosts}");
        }
    }


}
