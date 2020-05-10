#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Caliburn.Micro;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Controls
{
    public class DialogComboBoxSelectionViewModel
    {
        #region Fields

        readonly IEventAggregator _eventAggregator;
        readonly ILocalizationHelper _localizationHelper;
        string _helperTextResourceKey;
        string _hintResourceKey;
        string _headerResourceKey;

        #endregion

        #region Properties

        public BindableCollection<IViewModelBase> Items { get; set; }

        [AlsoNotifyFor("CanAccept")] public int SelectedIndex { get; set; } = -1;

        public bool CanAccept => SelectedIndex > -1;

        public bool CanCancel { get; set; }

        public string HelperText => _localizationHelper.GetByKey(_helperTextResourceKey);

        public string Hint => _localizationHelper.GetByKey(_helperTextResourceKey);

        public string Header => string.IsNullOrEmpty(_headerResourceKey) ? "" : _localizationHelper.GetByKey(_headerResourceKey);

        #endregion

        #region ctor
        public DialogComboBoxSelectionViewModel(IEventAggregator aggregator, ILocalizationHelper loc)
        {
            _eventAggregator = aggregator;
            _localizationHelper = loc;
        }

        #endregion

        #region Methods

        public void Init(IEnumerable<IViewModelBase> items, bool canCancel = true, string helperTextResourceKey = "SelectAnItem", string hintResourceKey = "Item", string headerResourceKey = "")
        {
            Items = new BindableCollection<IViewModelBase>();
            Items.AddRange(items.ToList());
            CanCancel = canCancel;

            _helperTextResourceKey = helperTextResourceKey;
            _hintResourceKey = hintResourceKey;
            _headerResourceKey = headerResourceKey;
        }

        public virtual void Cancel()
        {
            if (!CanCancel)
                return;
            _eventAggregator.PublishOnUIThreadAsync(new DialogSelectedMessage());
        }

        public virtual void Accept()
        {
            _eventAggregator.PublishOnCurrentThreadAsync(new DialogSelectedMessage(Items[SelectedIndex]));
        }
        
        #endregion
    }
}