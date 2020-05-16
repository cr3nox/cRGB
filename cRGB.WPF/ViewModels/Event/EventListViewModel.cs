#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.Base;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Controls;
using cRGB.WPF.ViewModels.Event.Events;

namespace cRGB.WPF.ViewModels.Event
{
    public class EventListViewModel : ViewModelBase, IEventListViewModel, IHandle<DialogSelectedMessage>, IDisposable
    {
        #region Fields

        readonly IEventAggregator _eventAggregator;
        readonly IEventViewModelFactory _eventFactory;

        #endregion

        #region Properties

        public BindableCollection<IEventViewModel> Events { get; set; }= new BindableCollection<IEventViewModel>();

        public IEventViewModel SelectedEvent { get; set; }

        public DialogComboBoxSelectionViewModel DialogComboBoxSelectionViewModel { get; }

        public bool IsEventSelectionOpen { get; set; }

        public List<ILedEvent> EventSettings { get; internal set; } = new List<ILedEvent>();

        #endregion

        #region Constructor
        public EventListViewModel(IEventAggregator aggregator, IEventViewModelFactory eventFactory, DialogComboBoxSelectionViewModel dialogViewModel)
        {
            _eventAggregator = aggregator;
            _eventFactory = eventFactory;
            _eventAggregator.SubscribeOnUIThread(this);

            DialogComboBoxSelectionViewModel = dialogViewModel;
            // Gets one Instance of each IEventViewModel
            //var x = _eventFactory.Create(typeof(TimerEventViewModel));
            DialogComboBoxSelectionViewModel.Init(_eventFactory.CreateForEachImplementation(), 
                true, "SelectAnEvent", headerResourceKey: "Events");
        }

        #endregion

        #region Methods

        public void AddEvent()
        {
            // Gets one Instance of each IEventViewModel
            DialogComboBoxSelectionViewModel.Init(_eventFactory.CreateForEachImplementation(), false, "SelectAnEvent", headerResourceKey: "Events");
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
                var vm = _eventFactory.Create(eventSetting.EventType);
                if (!(vm is { } eventViewModel)) continue;
                eventViewModel.Model = eventSetting;
                Events.Add(eventViewModel);
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
        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
            foreach (var viewModelBase1 in DialogComboBoxSelectionViewModel.Items)
            {
                _eventFactory.Release((IEventViewModel)viewModelBase1);
            }
        }

        #endregion


    }
}