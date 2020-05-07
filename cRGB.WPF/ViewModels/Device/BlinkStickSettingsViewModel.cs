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
        public IBlinkStickSettings BlinkStickSettings;

        public string DeviceName
        {
            get => BlinkStickSettings.Name;
            set => BlinkStickSettings.Name = value;
        }

        public string SerialNumber
        {
            get => BlinkStickSettings.SerialNumber;
            set => BlinkStickSettings.SerialNumber = value;
        }

        public string Description
        {
            get => BlinkStickSettings.Description;
            set => BlinkStickSettings.Description = value;
        }

        public int RChannelLedCount
        {
            get => BlinkStickSettings.RChannelLedCount;
            set
            {
                if (value < 0)
                    BlinkStickSettings.RChannelLedCount = 0;
                else if (value > 64)
                    BlinkStickSettings.RChannelLedCount = 64;
                BlinkStickSettings.RChannelLedCount = value;
            }
        }

        public int GChannelLedCount
        {
            get => BlinkStickSettings.GChannelLedCount;
            set
            {
                if (value < 0)
                    BlinkStickSettings.GChannelLedCount = 0;
                else if (value > 64)
                    BlinkStickSettings.GChannelLedCount = 64;
                BlinkStickSettings.GChannelLedCount = value;
            }
        }

        public int BChannelLedCount
        {
            get => BlinkStickSettings.BChannelLedCount;
            set
            {
                if (value < 0)
                    BlinkStickSettings.BChannelLedCount = 0;
                else if (value > 64)
                    BlinkStickSettings.BChannelLedCount = 64;
                BlinkStickSettings.BChannelLedCount = value;
            }
        }

        public int LedCount => RChannelLedCount + GChannelLedCount + BChannelLedCount;

        public bool RChannelLedInvert
        {
            get => BlinkStickSettings.RChannelLedInvert;
            set => BlinkStickSettings.RChannelLedInvert = value;
        }

        public bool GChannelLedInvert
        {
            get => BlinkStickSettings.GChannelLedInvert;
            set => BlinkStickSettings.GChannelLedInvert = value;
        }

        public bool BChannelLedInvert
        {
            get => BlinkStickSettings.BChannelLedInvert;
            set => BlinkStickSettings.BChannelLedInvert = value;
        }

        public bool CombineChannels
        {
            get => BlinkStickSettings.CombineChannels;
            set => BlinkStickSettings.CombineChannels = value;
        }

        public BindableCollection<int> DisabledLeds
        {
            get => new BindableCollection<int>(BlinkStickSettings.DisabledLeds);
            set => BlinkStickSettings.DisabledLeds = value.ToList();
        }

        public int Brightness
        {
            get => BlinkStickSettings.Brightness;
            set => BlinkStickSettings.Brightness = value;
        }
        
        public BindableCollection<LedViewModel> RChannelLedColors { get; set; }
        public BindableCollection<LedViewModel> GChannelLedColors { get; set; }
        public BindableCollection<LedViewModel> BChannelLedColors { get; set; }

        public BlinkStickSettingsViewModel(IBlinkStickSettings settings)
        {
            BlinkStickSettings = settings;
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
