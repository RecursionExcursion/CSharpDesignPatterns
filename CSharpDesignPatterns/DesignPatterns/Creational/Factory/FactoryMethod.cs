using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.CreationalPatterns.Factory
{
    /*
     * Product
     */

    internal abstract class DiscountService
    {
        public abstract int DiscountPercentage
        {
            get;
        }

        public override string ToString() => GetType().Name;
    }

    /*
     * ConcreteProducts
     */

    internal class CountryDiscountService : DiscountService
    {

        private readonly string _countryIdentifier;

        public CountryDiscountService(string countryIdentifier)
        {
            _countryIdentifier = countryIdentifier;
        }

        public override int DiscountPercentage
        {
            get
            {
                switch (_countryIdentifier)
                {
                    case "BE":
                        return 20;
                    default:
                        return 10;
                }
            }
        }
    }
    internal class CodeDiscountService : DiscountService
    {

        private readonly Guid code;

        public CodeDiscountService(Guid code)
        {
            this.code = code;
        }

        public override int DiscountPercentage
        {
            get => 15;
        }
    }


    /*
     * Creator
     */

    internal abstract class DiscountFacory
    {
        public abstract DiscountService CreateDiscountService();
    }

    /*
     * ConcreteCreators
     */

    internal class CountryDiscountFacory : DiscountFacory
    {

        private readonly string _countryIdentifier;

        public CountryDiscountFacory(string countryIdentifier)
        {
            _countryIdentifier = countryIdentifier;
        }

        public override DiscountService CreateDiscountService()
        {
            return new CountryDiscountService(_countryIdentifier);
        }
    }
    internal class CodeDiscountFacory : DiscountFacory
    {

        private readonly Guid _code;

        public CodeDiscountFacory(Guid code)
        {
            _code = code;
        }

        public override DiscountService CreateDiscountService()
        {
            return new CodeDiscountService(_code);
        }
    }

}
