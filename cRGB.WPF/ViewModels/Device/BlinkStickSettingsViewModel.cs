using System.Linq;
using System.Xml.Serialization;
using Caliburn.Micro;
using cRGB.Domain.Models.Device;
using cRGB.Tools.Interfaces.ViewModel;

namespace cRGB.WPF.ViewModels.Device
{
    public class BlinkStickSettingsViewModel : ViewModelBase
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
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

        public int LedCount => RChannelLedCount + GChannelLedCount + BChannelLedCount;

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
        
        public BindableCollection<LedViewModel> RChannelLedColors { get; set; }
        public BindableCollection<LedViewModel> GChannelLedColors { get; set; }
        public BindableCollection<LedViewModel> BChannelLedColors { get; set; }

        public BlinkStickSettingsViewModel()
        {

        }

        public void EnableAllRChannelLedColors()
        {
            foreach (var led in RChannelLedColors)
            {
                led.Enabled = true;
            }
        }

        public void DisableAllRChannelLedColors()
        {
            foreach (var led in RChannelLedColors)
            {
                led.Enabled = false;
            }
        }

        public void EnableAllGChannelLedColors()
        {
            foreach (var led in GChannelLedColors)
            {
                led.Enabled = true;
            }
        }

        public void DisableAllGChannelLedColors()
        {
            foreach (var led in GChannelLedColors)
            {
                led.Enabled = false;
            }
        }

        public void DisableAllBChannelLedColors()
        {
            foreach (var led in BChannelLedColors)
            {
                led.Enabled = false;
            }
        }

        public void EnableAllBChannelLedColors()
        {
            foreach (var led in BChannelLedColors)
            {
                led.Enabled = true;
            }
        }

        public void RefreshProperties()
        {
            foreach (var ledState in RChannelLedColors)
            {
                ledState.FirePropertyChanged();
            }
        }
    }
}
