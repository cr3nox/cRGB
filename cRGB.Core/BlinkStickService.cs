using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BlinkStickDotNet;

namespace cRGB.Domain
{
    public class BlinkStickService
    {
        public ObservableCollection<BlinkStick> BlinkSticks = new ObservableCollection<BlinkStick>();

        public BlinkStickService()
        {

        }

        public List<BlinkStick> FindAll()
        {
            return BlinkStick.FindAll().ToList();
        }

        public List<BlinkStick> FindAllNotAlreadyConfigured()
        {
            var sticks = FindAll();
            return sticks.Where(o => BlinkSticks.All(b => b.Serial != o.Serial)).ToList();
        }

        public BlinkStick Find(string serial)
        {
            return BlinkStick.FindBySerial(serial);
        }

        public BlinkStick AddBlinkStick(string serial)
        {
            var stick = BlinkStick.FindBySerial(serial);
            BlinkSticks.Add(stick);
            return stick;
        }

        public void AddBlinkStick(BlinkStick stick)
        {
            BlinkSticks.Add(stick);
        }

        public ObservableCollection<BlinkStick> GetAllConfigured()
        {
            return BlinkSticks;
        }
    }
}
