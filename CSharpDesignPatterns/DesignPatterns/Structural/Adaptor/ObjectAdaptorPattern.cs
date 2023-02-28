using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Structural.Adaptor
{

    /*
     * AKA WrapperPattern
     * Provides a different interface for the client accessing the object
     */

    /*
     * ObjectAdaptorPattern
     */
    public class CityFromExternalSystem
    {

        public string Name { get; private set; }
        public string NickName { get; private set; }
        public int Inhabitants { get; private set; }

        public CityFromExternalSystem(string name, string nickName, int inhabitants)
        {
            Name = name;
            NickName = nickName;
            Inhabitants = inhabitants;
        }
    }

    /*
     * Adaptee
     */

    public class ExternalSystem
    {
        public CityFromExternalSystem GetCity()
        {
            return new CityFromExternalSystem("Detroit", "D-Town", 2000000);
        }
    }

    public class City
    {
        public string FullName { get; private set; }
        public long Inhabitants { get; private set; }

        public City(string fullName, long inhabitants)
        {
            FullName = fullName;
            Inhabitants = inhabitants;
        }
    }

    /*
     *Target
     */

    public interface ICityAdaptor
    {
        City GetCity();
    }

    /*
     * Adapter
     */
    public class CityAdaptor : ICityAdaptor
    {
        public ExternalSystem ExternalSystem { get; private set; } = new();
        public City GetCity()
        {
            var cityFromExernalSystem = ExternalSystem.GetCity();
            return new($"{cityFromExernalSystem.Name} - {cityFromExernalSystem.NickName}", cityFromExernalSystem.Inhabitants);
        }
    }
}
