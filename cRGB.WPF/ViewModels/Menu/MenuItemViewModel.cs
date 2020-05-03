using System.ComponentModel;
using Caliburn.Micro;
using cRGB.WPF.Messages;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Menu
{
    public class MenuItemViewModel : ViewModelBase
    {
        #region Properties

        readonly IEventAggregator _eventAggregator;

        ViewModelBase _viewModel;
        public ViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                ViewModel.PropertyChanged += ViewModelPropertyChanged;
            }

        }

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

        public string Header
        {
            get => ViewModel.DisplayName;
            set => ViewModel.DisplayName = value;
        }

        #endregion

        #region Constructor

        public MenuItemViewModel(IEventAggregator aggregator)
        {
            _eventAggregator = aggregator;
        }

        #endregion


        void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Header)));
        }

        public void Create(ViewModelBase viewModel, MenuItemViewModel parent = null, PackIconKind icon = PackIconKind.None, string displayName = null, bool isExpanded = true)
        {
            ViewModel = viewModel;
            ParentMenuItem = parent;
            IsExpanded = isExpanded;
            Icon = icon;
            if (!string.IsNullOrEmpty(displayName)) ViewModel.DisplayName = displayName;
        }

        public MenuItemViewModel CreateChild(ViewModelBase viewModel, PackIconKind icon = PackIconKind.None, bool isExpanded = true, string displayName = null)
        {
            var child = IoC.Get<MenuItemViewModel>();
            child.Create(viewModel, this, icon, displayName, true);
            child.ViewModel = viewModel;
            child.ParentMenuItem = this;
            child.Icon = icon;
            child.IsExpanded = isExpanded;

            Children.Add(child);
            return child;
        }

    }
}
