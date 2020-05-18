#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using cRGB.Modules.Common.Base;

namespace cRGB.WPF.ViewModels.Effect.Effects
{
    public interface IEffectViewModel: IViewModelBase
    {
        public byte[] Tick(int ledCount);
    }
}