#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Reflection;
using Castle.Facilities.TypedFactory;

namespace cRGB.WPF.ServiceLocation.Selectors
{
    public class ClassByNameFactoryComponentSelector : DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (method.Name == "Create" && arguments.Length >= 1 && arguments[0] is Type type)
            {
                return type.Name;
            }
            return base.GetComponentName(method, arguments);
        }
    }
}