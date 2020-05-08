using System.Collections.Generic;
using System.Collections.ObjectModel;
using BlinkStickDotNet;

namespace cRGB.Domain.Services.Device
{
    public interface IBlinkStickService
    {
        public ObservableCollection<BlinkStick> BlinkSticks { get; set; }

        public List<BlinkStick> FindAll();

        public List<BlinkStick> FindAllNotAlreadyConfigured();

        public BlinkStick Find(string serial);

        public BlinkStick AddBlinkStick(string serial);

        public void AddBlinkStick(BlinkStick stick);

        public ObservableCollection<BlinkStick> GetAllConfigured();
    }
}
