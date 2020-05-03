using System;
using System.Linq;
using System.Windows.Documents;
using System.Xml.Serialization;
using Caliburn.Micro;
using cRGB.Domain.Models.Device;

namespace cRGB.WPF.ViewModels.Device
{
    public class BlinkStickSettingsViewModel : ViewModelBase
    {
        IBlinkStickSettings _blinkStickSettings = new BlinkStickSettings();

        public string DeviceName
        {
            get => _blinkStickSettings.Name;
            set => _blinkStickSettings.Name = value;
        }

        public string SerialNumber
        {
            get => _blinkStickSettings.SerialNumber;
            set => _blinkStickSettings.SerialNumber = value;
        }

        public string Description
        {
            get => _blinkStickSettings.Description;
            set => _blinkStickSettings.Description = value;
        }

        public int RChannelLedCount
        {
            get => _blinkStickSettings.RChannelLedCount;
            set
            {
                if (value < 0)
                    _blinkStickSettings.RChannelLedCount = 0;
                else if (value > 64)
                    _blinkStickSettings.RChannelLedCount = 64;
                _blinkStickSettings.RChannelLedCount = value;
            }
        }

        public int GChannelLedCount
        {
            get => _blinkStickSettings.GChannelLedCount;
            set
            {
                if (value < 0)
                    _blinkStickSettings.GChannelLedCount = 0;
                else if (value > 64)
                    _blinkStickSettings.GChannelLedCount = 64;
                _blinkStickSettings.GChannelLedCount = value;
            }
        }

        public int BChannelLedCount
        {
            get => _blinkStickSettings.BChannelLedCount;
            set
            {
                if (value < 0)
                    _blinkStickSettings.BChannelLedCount = 0;
                else if (value > 64)
                    _blinkStickSettings.BChannelLedCount = 64;
                _blinkStickSettings.BChannelLedCount = value;
            }
        }

        public bool RChannelLedInvert
        {
            get => _blinkStickSettings.RChannelLedInvert;
            set => _blinkStickSettings.RChannelLedInvert = value;
        }

        public bool GChannelLedInvert
        {
            get => _blinkStickSettings.GChannelLedInvert;
            set => _blinkStickSettings.GChannelLedInvert = value;
        }

        public bool BChannelLedInvert
        {
            get => _blinkStickSettings.GChannelLedInvert;
            set => _blinkStickSettings.GChannelLedInvert = value;
        }

        public BindableCollection<int> DisabledLeds
        {
            get => new BindableCollection<int>(_blinkStickSettings.DisabledLeds);
            set => _blinkStickSettings.DisabledLeds = value.ToList();
        }




        [XmlIgnore]
        public BlinkStickViewModel ParentViewModel { get; set; }

        public BindableCollection<Led> RChannelLedColors
        {
            get => ParentViewModel.RChannelLedColors;
            set => ParentViewModel.RChannelLedColors = value;
        }
        public BindableCollection<Led> GChannelLedColors
        {
            get => ParentViewModel.GChannelLedColors;
            set => ParentViewModel.GChannelLedColors = value;
        }
        public BindableCollection<Led> BChannelLedColors
        {
            get => ParentViewModel.BChannelLedColors;
            set => ParentViewModel.BChannelLedColors = value;
        }

        public BlinkStickSettingsViewModel()
        {

        }
    }
}
