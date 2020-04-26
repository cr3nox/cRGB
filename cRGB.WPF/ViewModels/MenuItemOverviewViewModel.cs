using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using cRGB.Core;

namespace cRGB.WPF.ViewModels
{
    public sealed class MenuItemOverviewViewModel : MenuItemViewModel
    {
        public BindableCollection<LedDeviceViewModel> LedDevices { get; set; } = new BindableCollection<LedDeviceViewModel>();
        public LedDeviceViewModel SelectedLedDevice { get; set; }

        BlinkStickController _blinkStickController;

        public MenuItemOverviewViewModel(BlinkStickController blinkStickController)
        {
            DisplayName = "Overview";
            _blinkStickController = blinkStickController;
        } 

        public void AddBlinkStick()
        {
            var x = _blinkStickController.GetAllConnected();
            var newDevice = new BlinkStickViewModel(){Name = "New Device"};
            LedDevices.Add(newDevice);
            SelectedLedDevice = newDevice;
        }

        public void AddArduino()
        {
            //throw new NotImplementedException();
        }

        public void AddTestDevice()
        {
            //throw new NotImplementedException();
        }
    }
}
