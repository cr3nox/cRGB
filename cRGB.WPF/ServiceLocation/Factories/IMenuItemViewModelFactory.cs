#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using cRGB.WPF.ViewModels.Event;
using cRGB.WPF.ViewModels.Navigation;

namespace cRGB.WPF.ServiceLocation.Factories
{
    public interface IMenuItemViewModelFactory
    {
        IMenuItemViewModel Create();
        void Release(IMenuItemViewModel menuItemViewModel);
    }
}