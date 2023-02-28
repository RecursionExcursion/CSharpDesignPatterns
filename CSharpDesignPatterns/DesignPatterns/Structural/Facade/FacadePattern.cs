using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Structural.Facade
{
    /*
     * Facade
     */

    public class DiscountFacade
    {
        private readonly OrderService _orderService = new();
        private readonly CustomerDiscountBaseService _customerDiscountBaseService = new();
        private readonly DayOfTheWeekFactorService _dayOfTheWeekFactorService = new();

        public double CalculateDiscountPercentage(int customerID)
        {
            if (!_orderService.HasEnoughOrders(customerID))
            {
                return 0;
            } else
            {
                return _customerDiscountBaseService.CalculateDiscountBase(customerID) *
                    _dayOfTheWeekFactorService.CalculateDayOfTheWeekFactor();
            }
        }

    }

    /*
     * SubSystemClasses
     */
    public class OrderService
    {
        public bool HasEnoughOrders(int customerID)
        {
            //Fake calcualtions
            return customerID > 5;
        }
    }

    public class CustomerDiscountBaseService
    {
        public double CalculateDiscountBase(int customerID)
        {
            //fake calculations
            return customerID > 8 ? 10 : 20;
        }
    }

    public class DayOfTheWeekFactorService
    {
        public double CalculateDayOfTheWeekFactor()
        {
            //Fake calculations
            switch (DateTime.UtcNow.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday:
                    return .8;
                default:
                    return 1.2;
            }
        }
    }
}
