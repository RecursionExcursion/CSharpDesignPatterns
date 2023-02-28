using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Structural.Bridge
{
    /*
     * Abstraction
     */
    public abstract class Menu
    {
        public readonly ICoupon _coupon = null!;
        public Menu(ICoupon coupon)
        {
            _coupon = coupon;
        }
        public abstract int CalculatePrice();
    }

    /*
     * RefinedAbstraction
     */

    public class VegitarianMenu : Menu
    {
        public VegitarianMenu(ICoupon coupon) : base(coupon)
        {
        }

        public override int CalculatePrice()
        {
            return 20 - _coupon.CouponValue;
        }
    }
    public class MeatMenu : Menu
    {
        public MeatMenu(ICoupon coupon) : base(coupon)
        {
        }

        public override int CalculatePrice()
        {
            return 30 - _coupon.CouponValue;
        }
    }


    /*
     * Implementor
     */
    public interface ICoupon
    {
        int CouponValue { get; }
    }

    /*
     * ConcreteImplementor
     */

    public class NoCoupon : ICoupon
    {
        public int CouponValue => 0;
    }
    public class OneDollarCoupon : ICoupon
    {
        public int CouponValue => 1;
    }
    public class TwoDollarCoupon : ICoupon
    {
        public int CouponValue => 2;
    }
}
