#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Controls;

namespace cRGB.WPF.ViewModels.Event
{
    public class EventListViewModel : ViewModelBase, IEventListViewModel, IHandle<DialogSelectedMessage>
    {
        #region Fields

        IEventAggregator _eventAggregator;


        #endregion

        #region Properties

        public BindableCollection<IEventViewModel> Events { get; set; }= new BindableCollection<IEventViewModel>();

        public IEventViewModel SelectedEvent { get; set; }

        public DialogComboBoxSelectionViewModel DialogComboBoxSelectionViewModel { get; }

        public bool IsEventSelectionOpen { get; set; }

        #endregion

        #region Constructor
        public EventListViewModel(IEventAggregator aggregator, DialogComboBoxSelectionViewModel dialogViewModel)
        {
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);

            // Gets one Instance of each IEventViewModel
            dialogViewModel.Init(IoC.GetAllInstances(typeof(IEventViewModel)).OfType<IEventViewModel>()
                .ToList(), true, "SelectAnEvent", headerResourceKey: "Events");

            DialogComboBoxSelectionViewModel = dialogViewModel;
        }

        #endregion

        #region Methods

        public void AddEvent()
        {
            IsEventSelectionOpen = true;
        }

        public Task HandleAsync(DialogSelectedMessage message, CancellationToken cancellationToken)
        {
            if (message.SelectedViewModel != null)
            {
                var eventViewModel = (IEventViewModel) message.SelectedViewModel;
                Events.Add(eventViewModel);
                SelectedEvent = eventViewModel;
            }
            
            IsEventSelectionOpen = false;
            return Task.CompletedTask;
        }

        #endregion


    }
}