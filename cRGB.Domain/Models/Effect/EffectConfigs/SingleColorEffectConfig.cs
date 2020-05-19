#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Drawing;

namespace cRGB.Domain.Models.Effect.EffectConfigs
{
    public class SingleColorLedEffect : LedEffect, ISingleColorLedEffect
    { 
        public Color Color { get; set; }
        public bool Randomize { get; set; } = false;
    }

    public interface ISingleColorLedEffect : ILedEffect
    {
        public Color Color { get; set; }
        public bool Randomize { get; set; }
    }
}