#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using cRGB.Domain.Models.Device;
using cRGB.WPF.ViewModels.Device;

namespace cRGB.WPF.ServiceLocation.Factories
{
    public interface ILedViewModelFactory
    {
        ILedViewModel Create();
        void Release(ILedViewModel ledViewModel);
    }
}