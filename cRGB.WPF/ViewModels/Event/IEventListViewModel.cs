#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using Caliburn.Micro;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.Base;
using cRGB.WPF.ViewModels.Controls;

namespace cRGB.WPF.ViewModels.Event
{
    public interface IEventListViewModel : IViewModelBase
    {
        public BindableCollection<IEventViewModel> Events { get; set; }

        public IEventViewModel SelectedEvent { get; set; }

        public IDialogComboBoxSelectionViewModel DialogComboBoxSelectionViewModel { get; }

        public bool IsEventSelectionOpen { get; set; }

        public IList<ILedEvent> EventSettings { get; }

        public void InitSettings(IList<ILedEvent> settings = null);

        public void SetHighestLedIndex(int index);
    }
}