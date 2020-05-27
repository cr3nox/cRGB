#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cRGB.Domain.Models.Effect;
using cRGB.Modules.Common.Base;
using cRGB.WPF.ViewModels.Device;

namespace cRGB.WPF.ViewModels.Effect
{
    public interface IEffectViewModel: IViewModelBase, IDisposable
    {
        public ILedEffect Config { get; }
        public abstract Task<IList<ILedViewModel>> TickAsync(CancellationToken ct, IList<ILedViewModel> ledCount);

    }
}