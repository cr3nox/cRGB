using System.ComponentModel;
using Caliburn.Micro;
using cRGB.Modules.Common.Base;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Navigation
{
    public interface IMenuItemViewModel : IViewModelBase
    {
       public IViewModelBase ViewModel { get; set; }

        public IMenuItemViewModel ParentMenuItem { get; set; }

        public BindableCollection<IMenuItemViewModel> Children { get; set; } 

        public bool IsExpanded { get; set; }

        public bool IsSelected { get; set; }
        
        public PackIconKind Icon { get; set; }

        public string Header { get; set; }


        void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e);

        public void CreateMenu(IViewModelBase viewModel, IMenuItemViewModel parent = null,
            PackIconKind icon = PackIconKind.None, string displayName = null, bool isExpanded = true);

        public IMenuItemViewModel CreateChild(IViewModelBase viewModel, PackIconKind icon = PackIconKind.None,
            bool isExpanded = true, string displayName = null);

    }
}
