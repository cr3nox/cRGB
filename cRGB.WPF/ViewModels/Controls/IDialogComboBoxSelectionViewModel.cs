#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

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
    public interface IDialogComboBoxSelectionViewModel : IViewModelBase
    {
        #region Properties

        public BindableCollection<IViewModelBase> Items { get; set; }
        public BindableCollection<string> ItemDisplayNames { get; }
        public bool CanAccept { get; }
        public int SelectedIndex { get; set; }
        public bool CanCancel { get; set; }
        public string HelperText { get; }
        public string Hint { get; }
        public string Header { get; }

        #endregion

        #region Methods

        public void Init(
            IEnumerable<IViewModelBase> items, bool canCancel = true, object tag = null,
            string helperTextResourceKey = "SelectAnItem", string hintResourceKey = "Item",
            string headerResourceKey = "");

        public abstract void Cancel();

        public abstract void Accept();

        #endregion

            
    }
}