#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Linq;
using Caliburn.Micro;

namespace cRGB.WPF.ViewModels.Event
{
    public class EventSelectionViewModel
    {

        IEventAggregator _eventAggregator;
        public BindableCollection<IEventViewModel> AvailableEvents { get; }

        public EventSelectionViewModel(IEventAggregator aggregator)
        {
            _eventAggregator = aggregator;
            var x = IoC.GetAllInstances(typeof(IEventViewModel)).ToList();
        }
    }
}