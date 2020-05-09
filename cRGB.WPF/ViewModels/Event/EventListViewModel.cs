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

        public EventListViewModel()
        {

        }
        public void AddEvent()
        {

        }
    }
}