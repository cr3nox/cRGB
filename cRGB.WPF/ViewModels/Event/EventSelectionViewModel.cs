#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Linq;
using Caliburn.Micro;
using Castle.Windsor;

namespace cRGB.WPF.ViewModels.Event
{
    public class EventSelectionViewModel
    {

        IEventAggregator _eventAggregator;
        public BindableCollection<IEventViewModel> AvailableEvents { get; }

        public EventSelectionViewModel(IEventAggregator aggregator)
        {
            _eventAggregator = aggregator;
            // Gets one Instance of each IEventViewModel
            AvailableEvents = new BindableCollection<IEventViewModel>();
            AvailableEvents.AddRange(IoC.GetAllInstances(typeof(IEventViewModel)).OfType<IEventViewModel>().ToList());
        }
    }
}