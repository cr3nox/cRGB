#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

namespace cRGB.WPF.ViewModels.Event.Events
{
    public class StaticEventViewModel : EventViewModel
    {
        // Static event always returns true
        public override bool CanActivate => true;
    }
}