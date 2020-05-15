#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.ViewModelBase;

namespace cRGB.WPF.ViewModels.Event
{
    public interface IEventListViewModel : IViewModelBase
    {
        public void InitSettings(IList<ILedEvent> settings = null);
    }
}