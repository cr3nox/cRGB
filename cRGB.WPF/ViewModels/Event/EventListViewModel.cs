#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.Base;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Controls;
using cRGB.WPF.ViewModels.Event.Events;
using GongSolutions.Wpf.DragDrop;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Event
{
    public class EventListViewModel : ViewModelBase, IEventListViewModel, IHandle<DialogSelectedMessage>, IDisposable, IDropTarget
    {
        #region Fields

        readonly IEventAggregator _eventAggregator;
        readonly IEventViewModelFactory _eventFactory;

        #endregion

        #region Properties

        [AlsoNotifyFor(nameof(EventSettings))]
        public BindableCollection<IEventViewModel> Events { get; set; } = new BindableCollection<IEventViewModel>();

        public IEventViewModel SelectedEvent { get; set; }

        public IDialogComboBoxSelectionViewModel DialogComboBoxSelectionViewModel { get; }

        public bool IsEventSelectionOpen { get; set; }

        public IList<ILedEvent> EventSettings { get; internal set; }

        #endregion

        #region Constructor
        public EventListViewModel(IEventAggregator aggregator, IEventViewModelFactory eventFactory, IDialogComboBoxSelectionViewModel dialogViewModel)
        {
            _eventAggregator = aggregator;
            _eventFactory = eventFactory;
            _eventAggregator.SubscribeOnUIThread(this);

            DialogComboBoxSelectionViewModel = dialogViewModel;

            // Gets one Instance of each IEventViewModel
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
                // I'm not confident with naming convention here so instead i save viewmodel name in Model
                // takes Model name (ie: StaticEvent and adds ViewModel => StaticEventViewModel)
                //var vm = _eventFactory.Create($"{eventSetting.GetType().Name.ToString()}ViewModel");
                var vm = _eventFactory.Create(eventSetting.EventType);
                if (!(vm is { } eventViewModel)) continue;
                eventViewModel.Model = eventSetting;
                eventViewModel.Init();
                Events.Add(eventViewModel);
            }
        }

        public void DeleteEvent(IEventViewModel eventViewModel)
        {
            EventSettings.Remove(eventViewModel.Model);
            if (SelectedEvent == eventViewModel)
                SelectedEvent = null;
            Events.Remove(eventViewModel);
            _eventFactory.Release(eventViewModel);
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

        #region DragDrop Events ListView

        public void DragOver(IDropInfo dropInfo)
        {
            if (!(dropInfo.Data is EventViewModel sourceItem) ||
                !(dropInfo.TargetItem is EventViewModel targetItem)) return;

            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            dropInfo.Effects = DragDropEffects.Move;
        }

        /// <summary>
        /// We have to use this to order the EventSettings model.
        /// The order of the events is important to save
        /// </summary>
        /// <param name="dropInfo"></param>
        public void Drop(IDropInfo dropInfo)
        {
            var viewModel = (IEventViewModel)dropInfo.Data;
            // Remove and Insert into Events BindableCollection
            Events.Remove(viewModel);
            Events.Insert(dropInfo.InsertIndex, viewModel);

            // Remove and Insert Model
            EventSettings.Remove(viewModel.Model);
            EventSettings.Insert(dropInfo.InsertIndex, viewModel.Model);
        }

        #endregion

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