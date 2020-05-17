#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using cRGB.Domain.Models.Event;
using cRGB.WPF.Messages;

namespace cRGB.WPF.ServiceLocation.Factories
{
    public interface ILedEventFactory
    {
        ILedEvent Create(Type type);
        void Release(ILedEvent ledEvent);
    }
}