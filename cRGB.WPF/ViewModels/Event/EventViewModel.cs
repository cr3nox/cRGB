#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.Domain.Models.Effect;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.Base;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Controls;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Effect;
using cRGB.WPF.ViewModels.Effect.Effects;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Event
{
    public abstract class EventViewModel : ViewModelBase, IHandle<DialogSelectedMessage>, IEventViewModel, IDisposable
    {
        #region Fields

        protected IEventAggregator EventAggregator;

        protected ILocalizationHelper Loc;
        protected IEffectViewModelFactory EffectViewModelFactory;
        #endregion

        #region Properties

        public abstract bool CanActivate { get; }
        public bool IsEnabled { get; set; } = true;
        public ILedEvent Model { get; set; }

        public BindableCollection<IEffectViewModel> Effects { get; set; }
        public IEffectViewModel SelectedEffect { get; set; }
        public IDialogComboBoxSelectionViewModel SelectionViewModel { get; set; }

        public bool IsEffectSelectionOpen { get; set; } = false;
        public int HighestLedIndex { get; set; } = 0;

        #endregion

        #region ctor

        protected EventViewModel(IEventAggregator aggregator, ILocalizationHelper loc, IEffectViewModelFactory effectViewModelFactory, 
            IDialogComboBoxSelectionViewModel selection, ILedEvent model)
        {
            EventAggregator = aggregator;
            Loc = loc;
            Model = model;
            EffectViewModelFactory = effectViewModelFactory;
            SelectionViewModel = selection;

            Effects = new BindableCollection<IEffectViewModel>();
        }

        #endregion

        #region Methods

        public virtual void Init()
        {
            EventAggregator.SubscribeOnUIThread(this);
            foreach (ILedEffect ledEffect in Model.LedEffects)
            {
                var vm = EffectViewModelFactory.Create(ledEffect.EffectType, ledEffect);
                Effects.Add(vm);
            }
        }

        public virtual IAsyncEnumerable<IList<ILedViewModel>> Activate(CancellationToken ct, IList<ILedViewModel> leds)
        {
            return null;
        }

        public virtual void Stop(){}

        public void AddEffect()
        {
            // Gets one Instance of each IEventViewModel
            SelectionViewModel.Init(EffectViewModelFactory.CreateForEachImplementation(), true,
                tag: this, helperTextResourceKey: "SelectAnEffect", headerResourceKey: "Effects");
            IsEffectSelectionOpen = true;
        }

        public void RemoveEffect(IEffectViewModel effectViewModel)
        {
            Model.LedEffects.Remove(effectViewModel.Config);
            Effects.Remove(effectViewModel);
            effectViewModel.Dispose();
        }

        public Task HandleAsync(DialogSelectedMessage message, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                if (message.SelectedViewModel != null && message.Tag == this)
                {
                    if (message.SelectedViewModel is IEffectViewModel vm)
                    {
                        // So User does not has to lookup index himself
                        vm.Config.LedEndIndex = HighestLedIndex;
                        Effects.Add(vm);
                        Model.LedEffects.Add(vm.Config);
                    }
                }
                IsEffectSelectionOpen = false;

            }, cancellationToken);
        }

        #endregion

        public void Dispose()
        {
            Model = null;
            EventAggregator.Unsubscribe(this);
        }
    }
}