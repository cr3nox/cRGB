using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using cRGB.WPF.Annotations;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Menu
{
    public class MenuItemViewModel : ViewModelBase
    {
        #region Properties

        readonly IEventAggregator _eventAggregator;
        readonly ILocalizationHelper _loc;
        public ViewModelBase ViewModel { get; set; }
        public MenuItemViewModel ParentMenuItem { get; set; }

        public BindableCollection<MenuItemViewModel> Children { get; set; } =
            new BindableCollection<MenuItemViewModel>();

        public bool IsExpanded { get; set; }
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                if(IsSelected)
                    _eventAggregator.PublishOnUIThreadAsync(new TreeViewSelectionChangedMessage(this));
            }
        }
        
        public PackIconKind Icon { get; set; }
        public string Header { get; set; }

        #endregion

        #region Constructor

        public MenuItemViewModel(IEventAggregator aggregator, ILocalizationHelper loc)
        {
            _eventAggregator = aggregator;
            _loc = loc;
        }

        #endregion


        public void Create(ViewModelBase viewModel, MenuItemViewModel parent = null, bool isExpanded = false, string header = "Menu Header", PackIconKind icon = PackIconKind.None)
        {
            ViewModel = viewModel;
            ParentMenuItem = parent;
            IsExpanded = isExpanded;
            Header = header;
            Icon = icon;
        }

        public MenuItemViewModel CreateChild(ViewModelBase viewModel, bool isExpanded = false, string header = "Menu Header", PackIconKind icon = PackIconKind.None)
        {
            var child = IoC.Get<MenuItemViewModel>();
            child.Create(viewModel, this, true, _loc.GetByKey("MenuItem_NewDevice"), PackIconKind.Abc);
            child.ViewModel = viewModel;
            child.ParentMenuItem = this;
            child.Header = header;
            child.Icon = icon;
            child.IsExpanded = isExpanded;

            Children.Add(child);
            return child;
        }

    }
}
