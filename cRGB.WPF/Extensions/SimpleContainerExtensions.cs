#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using cRGB.WPF.ViewModels.Event;

namespace cRGB.WPF.Extensions
{
    public static class SimpleContainerExtensions
    {

        /// <summary>
        /// Stolen from Caliburn.Micro SimpleContainer, It's now able to Register Per Request
        /// Key = nameof(Class)
        /// 
        /// Registers all specified types in an assembly as Per Request in the container.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="assembly">The assembly.</param>
        /// <param name="filter">The type filter.</param>
        /// <returns>The container.</returns>
        public static SimpleContainer AllTypesOfPerRequest<TService>(this SimpleContainer container, Assembly assembly,
            Func<Type, bool> filter = null)
        {
            if (filter == null)
            {
                filter = type => true;
            }

            var serviceType = typeof(TService);
            var types = from type in assembly.DefinedTypes
                where serviceType.GetTypeInfo().IsAssignableFrom(type)
                      && !type.IsAbstract
                      && !type.IsInterface
                      && filter(type.AsType())
                select type;

            foreach (var type in types)
            {
                container.RegisterPerRequest(typeof(TService), type.Name, type.AsType());
            }

            return container;
        }

        /// <summary>
        /// Get a Instance for each Implementation
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="container"></param>
        /// <param name="assembly"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IEnumerable<object> GetInstanceForEachImplementation<TService>(this SimpleContainer container, Type service, Assembly assembly, Func<Type, bool> filter = null)
        {
            if (filter == null)
            {
                filter = type => true;
            }

            var serviceType = typeof(TService);
            var types = from type in assembly.DefinedTypes
                where serviceType.GetTypeInfo().IsAssignableFrom(type)
                      && !type.IsAbstract
                      && !type.IsInterface
                      && filter(type.AsType())
                select type;

            var retList = new List<object>();
            foreach (var type in types)
            {
                retList.Add(container.GetInstance(typeof(TService), type.Name));
            }

            return retList;
        }
    }
}