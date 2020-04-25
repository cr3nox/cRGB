using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace cRGB.WPF.ViewModels
{
    public sealed class MenuItemOverviewViewModel : MenuItemViewModel
    {
        public BindableCollection<LedDeviceViewModel> LedDevices { get; set; }

        public MenuItemOverviewViewModel() => DisplayName = "Overview";

        public void AddDevice()
        {
            //LedDevices.Add();
        }
    }
}
