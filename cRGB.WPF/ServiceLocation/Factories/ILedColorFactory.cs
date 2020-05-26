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
    public interface ILedColorFactory
    {
        ILedColor Create();
        ILedColor Create(int r, int g, int b);
        ILedColor Create(int alpha, int r, int g, int b);
        ILedColor Create(byte r, byte g, byte b);
        ILedColor Create(byte alpha, byte r, byte g, byte b);
        void Release(ILedColor ledColor);
    }
}