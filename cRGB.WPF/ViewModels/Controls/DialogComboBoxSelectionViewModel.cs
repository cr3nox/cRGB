﻿#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Caliburn.Micro;
using cRGB.Modules.Common.Base;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Controls
{
    public class DialogComboBoxSelectionViewModel : ViewModelBase, IDialogComboBoxSelectionViewModel
    {
        #region Fields

        readonly IEventAggregator _eventAggregator;
        readonly ILocalizationHelper _localizationHelper;
        readonly IMessageFactory _messageFactory;
        string _helperTextResourceKey = "SelectAnItem";
        string _hintResourceKey = "Item";
        string _headerResourceKey = "";

        #endregion

        #region Properties

        [AlsoNotifyFor(nameof(ItemDisplayNames), nameof(CanAccept))]
        public BindableCollection<IViewModelBase> Items { get; set; }

        [AlsoNotifyFor(nameof(Items), nameof(CanAccept))]
        public BindableCollection<string> ItemDisplayNames => Items != null
            ? new BindableCollection<string>(Items.Select(o => o.DisplayName).ToList())
            : null;

        public bool CanAccept => SelectedIndex > -1;

        public int SelectedIndex { get; set; } = -1;

        public bool CanCancel { get; set; }

        public string HelperText => _localizationHelper.GetByKey(_helperTextResourceKey);

        public string Hint => _localizationHelper.GetByKey(_hintResourceKey);

        public string Header => string.IsNullOrEmpty(_headerResourceKey) ? "" : _localizationHelper.GetByKey(_headerResourceKey);

        object _tag;

        #endregion

        #region ctor
        public DialogComboBoxSelectionViewModel(IEventAggregator aggregator, ILocalizationHelper loc, IMessageFactory messageFactory)
        {
            _eventAggregator = aggregator;
            _localizationHelper = loc;
            _messageFactory = messageFactory;
        }

        #endregion

        #region Methods

        public void Init(IEnumerable<IViewModelBase> items, bool canCancel = true, object tag = null, string helperTextResourceKey = "SelectAnItem", string hintResourceKey = "Item", string headerResourceKey = "")
        {
            Items = new BindableCollection<IViewModelBase>(items.ToList());
            CanCancel = canCancel;

            _tag = tag;
            _helperTextResourceKey = helperTextResourceKey;
            _hintResourceKey = hintResourceKey;
            _headerResourceKey = headerResourceKey;
        }

        public virtual void Cancel()
        {
            if (!CanCancel) return;

            foreach (var viewModelBase in Items.OfType<IDisposable>())
            {
                viewModelBase.Dispose();
            }

            _eventAggregator.PublishOnUIThreadAsync(_messageFactory.Create(typeof(DialogSelectedMessage)));
        }

        public virtual void Accept()
        {
            if (!CanAccept) return;

            var message = (DialogSelectedMessage)_messageFactory.Create(typeof(DialogSelectedMessage));
            message.Tag = _tag;
            message.SelectedViewModel = Items[SelectedIndex];
            
            foreach (var viewModelBase in Items.OfType<IDisposable>().Where(o => o != message.SelectedViewModel))
            {
                viewModelBase.Dispose();
            }

            _eventAggregator.PublishOnCurrentThreadAsync(message);
        }
        
        #endregion
    }
}