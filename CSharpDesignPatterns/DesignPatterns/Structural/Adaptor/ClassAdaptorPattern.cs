﻿using System;
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
     * ClassAdaptorPattern
     */
    public class ClassCityFromExternalSystem
    {
        public string Name { get; private set; }
        public string NickName { get; private set; }
        public int Inhabitants { get; private set; }
        public ClassCityFromExternalSystem(string name, string nickName, int inhabitants)
        {
            Name = name;
            NickName = nickName;
            Inhabitants = inhabitants;
        }
    }

    /*
     * Adaptee
     */

    public class ClassExternalSystem
    {
        public ClassCityFromExternalSystem GetCity()
        {
            return new ClassCityFromExternalSystem("Detroit", "D-Town", 2000000);
        }
    }

    public class ClassCity
    {
        public string FullName { get; private set; }
        public long Inhabitants { get; private set; }

        public ClassCity(string fullName, long inhabitants)
        {
            FullName = fullName;
            Inhabitants = inhabitants;
        }
    }

    /*
     *Target
     */

    public interface IClassCityAdaptor
    {
        City GetCity();
    }

    /*
     * Adapter
     */

    public class ClassCityAdaptor : ClassExternalSystem, IClassCityAdaptor
    {
        public City GetCity()
        {
            var cityFromExernalSystem = base.GetCity();
            return new($"{cityFromExernalSystem.Name} - {cityFromExernalSystem.NickName}", cityFromExernalSystem.Inhabitants);
        }
    }
}
