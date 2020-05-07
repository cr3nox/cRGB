#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using cRGB.Domain.Models.Device;

namespace cRGB.WPF.ViewModels.Effect
{
    public class ChangeColorEffectViewModel : EffectViewModel
    {

        public IEnumerable<ILed> Tick(ILedChannel leds, object parameter);
    }
}