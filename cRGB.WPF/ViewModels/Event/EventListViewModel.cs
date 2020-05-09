#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Caliburn.Micro;
using cRGB.WPF.ViewModels.Event.Events;
using System.Linq;

namespace cRGB.WPF.ViewModels.Event
{
    public class EventListViewModel : ViewModelBase, IEventListViewModel
    {
        public IObservableCollection<IEventViewModel> ViewModels = new BindableCollection<IEventViewModel>();
        public void AddEvent()
        {
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(StaticEventViewModel)));
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(StaticEventViewModel)));
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(TimerEventViewModel)));
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(TimerEventViewModel)));
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(TimerEventViewModel)));
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(SpotifyEventViewModel)));
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(SpotifyEventViewModel)));
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(SpotifyEventViewModel)));
            ViewModels.Add(IoC.Get<IEventViewModel>(nameof(SpotifyEventViewModel)));
            var all = IoC.GetAll<IEventViewModel>().ToList();
            var x = IoC.Get<EventSelectionViewModel>();
            all = IoC.GetAll<IEventViewModel>().ToList();
        }
    }
}