#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.Effect;
using cRGB.Domain.Models.Event;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Effect;
using cRGB.WPF.ViewModels.Effect.Effects;
using cRGB.WPF.ViewModels.Event;

namespace cRGB.WPF.ServiceLocation.Factories
{
    public interface IEffectViewModelFactory
    {
        /// <summary>
        /// Creates an Instance of the given EventType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEffectViewModel Create(string type);
        IEffectViewModel Create(string type, ILedEffect eventConfig);

        /// <summary>
        /// Creates an Instance for each implementation of IEventViewModel that is registered in the Container
        /// </summary>
        /// <returns></returns>
        IEffectViewModel[] CreateForEachImplementation();
        void Release(IEffectViewModel viewModel);
    }
}