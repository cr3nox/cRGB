#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.ViewModelBase;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Controls;

namespace cRGB.WPF.ViewModels.Event
{
    public class EventListViewModel : ViewModelBase, IEventListViewModel, IHandle<DialogSelectedMessage>
    {
        #region Fields

        readonly IEventAggregator _eventAggregator;


        #endregion

        #region Properties

        public BindableCollection<IEventViewModel> Events { get; set; }= new BindableCollection<IEventViewModel>();

        public IEventViewModel SelectedEvent { get; set; }

        public DialogComboBoxSelectionViewModel DialogComboBoxSelectionViewModel { get; }

        public bool IsEventSelectionOpen { get; set; }

        public List<ILedEvent> EventSettings { get; internal set; } = new List<ILedEvent>();

        #endregion

        #region Constructor
        public EventListViewModel(IEventAggregator aggregator, DialogComboBoxSelectionViewModel dialogViewModel)
        {
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);

            DialogComboBoxSelectionViewModel = dialogViewModel;
            // Gets one Instance of each IEventViewModel
            DialogComboBoxSelectionViewModel.Init(IoC.GetAllInstances(typeof(IEventViewModel)).OfType<IEventViewModel>().ToList(), 
                true, "SelectAnEvent", headerResourceKey: "Events");
        }

        #endregion

        #region Methods

        public void AddEvent()
        {
            // Gets one Instance of each IEventViewModel
            DialogComboBoxSelectionViewModel.Init(IoC.GetAllInstances(typeof(IEventViewModel)).OfType<IEventViewModel>()
                .ToList(), true, "SelectAnEvent", headerResourceKey: "Events");
            IsEventSelectionOpen = true;
        }

        public void InitSettings(IList<ILedEvent> settings)
        {
            if (settings == null)
                return;

            EventSettings = (List<ILedEvent>)settings;

            // Create ViewModel for each saved config
            foreach (var eventSetting in EventSettings)
            {
                var vm = IoC.GetInstance(eventSetting.EventType, null);
                if (vm is IEventViewModel eventViewModel)
                {
                    eventViewModel.Model = eventSetting;
                    Events.Add(eventViewModel);
                }
            }
        }

        public Task HandleAsync(DialogSelectedMessage message, CancellationToken cancellationToken)
        {
            if (message.SelectedViewModel != null)
            {
                var eventViewModel = (IEventViewModel) message.SelectedViewModel;
                Events.Add(eventViewModel);
                SelectedEvent = eventViewModel;
                EventSettings.Add(eventViewModel.Model);
            }
            
            IsEventSelectionOpen = false;
            return Task.CompletedTask;
        }

        #endregion


    }
}