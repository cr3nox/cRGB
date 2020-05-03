using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BlinkStickDotNet;

namespace cRGB.Domain.Services
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
