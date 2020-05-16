#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using cRGB.Domain.Models.Device;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Event;

namespace cRGB.WPF.ServiceLocation.Factories
{
    public interface IEventViewModelFactory
    {
        /// <summary>
        /// Creates an Instance of the given EventType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEventViewModel Create(Type type);

        /// <summary>
        /// Creates an Instance for each implementation of IEventViewModel that is registered in the Container
        /// </summary>
        /// <returns></returns>
        IEventViewModel[] CreateForEachImplementation();
        void Release(IEventViewModel viewModel);
    }
}