#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Threading.Tasks;
using cRGB.Modules.Common.Base;

namespace cRGB.WPF.ViewModels.Effect.Effects
{
    public interface IEffectViewModel: IViewModelBase
    {
        public abstract Task<byte[]> TickAsync(int ledCount);
    }
}