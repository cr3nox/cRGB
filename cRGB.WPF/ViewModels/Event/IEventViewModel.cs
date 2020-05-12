#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

namespace cRGB.WPF.ViewModels.Event
{
    public interface IEventViewModel : IViewModelBase
    {
        public bool CanActivate { get; }
        public bool IsEnabled { get; set; }
    }
}