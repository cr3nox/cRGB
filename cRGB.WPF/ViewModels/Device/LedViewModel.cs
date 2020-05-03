using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Caliburn.Micro;
using cRGB.Domain.Models.Device;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Device
{
    public class LedViewModel : ViewModelBase
    {
        readonly IEventAggregator _eventAggregator;

        [AlsoNotifyFor("Colors")]
        public Led Led { get; set; }

        [AlsoNotifyFor("Led")]
        public bool Enabled
        {
            get => Led.Enabled;
            set => Led.Enabled = value;
        }

        public int Index
        {
            get => Led.Index;
            set => Led.Index = value;
        }

        [AlsoNotifyFor("Colors")]
        public int R
        {
            get => Led.R;
            set => Led.R = value;
        }

        [AlsoNotifyFor("Colors")]
        public int G
        {
            get => Led.G;
            set => Led.G = value;
        }

        [AlsoNotifyFor("Colors")]
        public int B
        {
            get => Led.B;
            set => Led.B = value;
        }

        public Color Colors => Led.GetLedAsColor;

        public LedViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Led = new Led();
        }

        public void SetLedColors(int r, int g, int b)
        {
            Led.R = r;
            Led.G = g;
            Led.B = b;
        }

        public void ActivationButton()
        {
            Enabled = !Enabled;
        }
    }
}
