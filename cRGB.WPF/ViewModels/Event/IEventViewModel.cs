#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.Base;

namespace cRGB.WPF.ViewModels.Event
{
    public interface IEventViewModel : IViewModelBase
    {
        public bool CanActivate { get; }
        public bool IsEnabled { get; set; }
        public ILedEvent Model { get; set; }

        public void Init();
    }
}