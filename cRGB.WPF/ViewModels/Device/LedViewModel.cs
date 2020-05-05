using System;
using System.ComponentModel;
using cRGB.Domain.Models.Device;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Device
{
    public class LedViewModel : ViewModelBase
    {
        [DoNotNotify] readonly Led _led;

        public bool Enabled
        {
            get => _led.Enabled;
            set => _led.Enabled = value;
        }

        [DoNotNotify]
        public int Index
        {
            get => _led.Index;
            set => _led.Index = value;
        }

        [DoNotNotify]
        public int R
        {
            get => _led.R;
            set => _led.R = value;
        }

        [DoNotNotify]
        public int G
        {
            get => _led.G;
            set => _led.G = value;
        }

        [DoNotNotify]
        public int B
        {
            get => _led.B;
            set => _led.B = value;
        }

        public byte[] GetLedAsByteArray => _led.GetLedAsByteArray;

        public LedViewModel()
        {
            _led = new Led();
        }

        public void SetLedColors(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
            //OnPropertyChanged(new PropertyChangedEventArgs(nameof(GetLedAsByteArray)));
        }

        public void FirePropertyChanged()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(GetLedAsByteArray)));
        }

        public void ActivationButton()
        {
            Enabled = !Enabled;
            var rnd = new Random();
            SetLedColors(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }
    }
}