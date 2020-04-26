using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BlinkStickDotNet;

namespace cRGB.Core
{
    public class BlinkStickController
    {
        public ObservableCollection<BlinkStick> BlinkSticks = new ObservableCollection<BlinkStick>();

        public BlinkStickController()
        {

        }

        public ObservableCollection<string> GetAllConnected()
        {
            var x = BlinkStick.FindAll();
            var dev = x.First();
            
            return null;
        }
    }
}
