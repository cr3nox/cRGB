#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using System.Threading;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.Base;
using cRGB.WPF.ViewModels.Device;

namespace cRGB.WPF.ViewModels.Event
{
    public interface IEventViewModel : IViewModelBase
    {
        public bool CanActivate { get; }
        public bool IsEnabled { get; set; }
        public ILedEvent Model { get; set; }
        public int HighestLedIndex { get; set; }

        public void Init();

        public abstract IAsyncEnumerable<IList<ILedViewModel>> Activate(CancellationToken ct, IList<ILedViewModel> leds);
        public abstract void Stop();
    }
}