#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using cRGB.Domain.Models.Device;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Event;

namespace cRGB.WPF.ServiceLocation.Factories
{
    public interface IMessageFactory
    {
        IMessage Create(Type type);
        void Release(IMessage message);
    }
}