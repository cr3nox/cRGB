using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Caliburn.Micro;

namespace cRGB.WPF.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// All Menu Items are declared here. (Left side navigation bar)
        /// </summary>
        public BindableCollection<MenuItemViewModel> MenuItems { get; } = new BindableCollection<MenuItemViewModel>();

        public MenuItemViewModel SelectedMenuItem { get; set; }

        public ShellViewModel(MenuItemOverviewViewModel overview)
        {
            MenuItems.Add(overview);
            SelectedMenuItem = overview;
        }

        #endregion Properties
    }
}
