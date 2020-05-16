using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BlinkStickDotNet;
using NotImplementedException = System.NotImplementedException;

namespace cRGB.Domain.Services.Device
{
    public class BlinkStickService : IBlinkStickService
    {
        public ObservableCollection<BlinkStick> BlinkSticks { get; set; } = new ObservableCollection<BlinkStick>();

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
            AddBlinkStick(stick);
            return stick;
        }

        public void AddBlinkStick(BlinkStick stick)
        {
            if (BlinkSticks.Any(o => o.Serial == stick.Serial))
                return;
            BlinkSticks.Add(stick);
        }

        public ObservableCollection<BlinkStick> GetAllConfigured()
        {
            return BlinkSticks;
        }

        public void Remove(string serial)
        {
            BlinkSticks.Remove(BlinkSticks.SingleOrDefault(o => o.Serial == serial));
        }
    }
}
