using System.ComponentModel;
using Caliburn.Micro;
using cRGB.Modules.Common.Base;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Navigation
{
    public class MenuItemViewModel : ViewModelBase, IMenuItemViewModel
    {
        #region Fields

        readonly IEventAggregator _eventAggregator;
        readonly IMenuItemViewModelFactory _menuFactory;

        #endregion

        #region Properties

        IViewModelBase _viewModel;
        public IViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                ViewModel.PropertyChanged += ViewModelPropertyChanged;
            }

        }

        public IMenuItemViewModel ParentMenuItem { get; set; }

        public BindableCollection<IMenuItemViewModel> Children { get; set; } =
            new BindableCollection<IMenuItemViewModel>();

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

        public string Header
        {
            get => ViewModel.DisplayName;
            set => ViewModel.DisplayName = value;
        }

        #endregion

        #region Constructor

        public MenuItemViewModel(IEventAggregator aggregator, IMenuItemViewModelFactory menuFactory)
        {
            _eventAggregator = aggregator;
            _menuFactory = menuFactory;
        }

        #endregion


        public void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Header)));
        }

        public void CreateMenu(IViewModelBase viewModel, IMenuItemViewModel parent = null, PackIconKind icon = PackIconKind.None, string displayName = null, bool isExpanded = true)
        {
            ViewModel = viewModel;
            ParentMenuItem = parent;
            IsExpanded = isExpanded;
            Icon = icon;
            if (!string.IsNullOrEmpty(displayName)) ViewModel.DisplayName = displayName;
        }

        public IMenuItemViewModel CreateChild(IViewModelBase viewModel, PackIconKind icon = PackIconKind.None, bool isExpanded = true, string displayName = null)
        {
            var child = _menuFactory.Create();
            child.CreateMenu(viewModel, this, icon, displayName, true);
            child.ViewModel = viewModel;
            child.ParentMenuItem = this;
            child.Icon = icon;
            child.IsExpanded = isExpanded;

            Children.Add(child);
            return child;
        }

    }
}
