using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace cRGB.WPF.ViewModels
{
    [Serializable]
    public class BlinkStickSettingsViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }

        private int _rChannelLedCount = 64;

        public int RChannelLedCount
        {
            get => _rChannelLedCount;
            set
            {
                if (value < 0)
                    _rChannelLedCount = 0;
                else if (value > 64)
                    _rChannelLedCount = 64;
                _rChannelLedCount = value;
            }
        }

        private int _gChannelLedCount = 64;
        public int GChannelLedCount
        {
            get => _gChannelLedCount;
            set
            {
                if (value < 0)
                    _gChannelLedCount = 0;
                else if (value > 64)
                    _gChannelLedCount = 64;
                _gChannelLedCount = value;
            }
        }
        private int _bChannelLedCount = 64;
        public int BChannelLedCount
        {
            get => _bChannelLedCount;
            set
            {
                if (value < 0)
                    _bChannelLedCount = 0;
                else if(value > 64)
                    _bChannelLedCount = 64;
                _bChannelLedCount = value;
            }
        }
        public bool RChannelLedInvert { get; set; } = false;
        public bool GChannelLedInvert { get; set; } = false;
        public bool BChannelLedInvert { get; set; } = false;

        public BindableCollection<int> IgnoredLeds { get; set; } = new BindableCollection<int>();

        public BlinkStickSettingsViewModel()
        {
        }
    }
}
