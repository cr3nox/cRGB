#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Reflection;
using Castle.Facilities.TypedFactory;

namespace cRGB.WPF.ServiceLocation.Selectors
{
    public class ClassByNameComponentSelector : DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (method.Name == "Create" && arguments.Length >= 1)
            {
                switch (arguments[0])
                {
                    case Type type:
                        return type.Name;
                    case string strType:
                        return strType;
                }
            }
            return base.GetComponentName(method, arguments);
        }
    }
}