using System;
using System.ComponentModel;
using cRGB.Domain.Models.Device;
using cRGB.Modules.Common.Base;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Device
{
    public interface  ILedViewModel : IViewModelBase
    {
        public bool Enabled { get; set; }
        public int Index { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public byte[] GetLedAsByteArray { get; }

        public void SetLedColors(int r, int g, int b);

        public void FirePropertyChanged();

        public void ActivationButton();
    }
}