#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using cRGB.Domain.Models.Device;

namespace cRGB.Domain.Models.Effect.EffectConfigs
{
    public class SingleColorLedEffect : LedEffect, ISingleColorLedEffect
    { 
        public ILedColor Color { get; set; }
        public bool Randomize { get; set; } = false;
    }

    public interface ISingleColorLedEffect : ILedEffect
    {
        public ILedColor Color { get; set; }
        public bool Randomize { get; set; }
    }
}