#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using cRGB.WPF.ViewModels.Device;

namespace cRGB.WPF.ServiceLocation.Factories
{
    public interface IBlinkStickViewModelFactory
    {
        BlinkStickViewModel Create();
        void Release(BlinkStickViewModel blinkStickViewModel);
    }
}