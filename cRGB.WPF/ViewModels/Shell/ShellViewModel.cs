using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Menu;
using MaterialDesignThemes.Wpf;
using Screen = Caliburn.Micro.Screen;

namespace cRGB.WPF.ViewModels.Shell
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<TreeViewSelectionChangedMessage>
    {
        readonly IEventAggregator _eventAggregator;

        #region Properties

        /// <summary>
        /// All Menu Items are declared here. (Left side navigation bar)
        /// </summary>
        public BindableCollection<MenuItemViewModel> MenuItems { get; } = new BindableCollection<MenuItemViewModel>();

        public MenuItemViewModel SelectedMenuItem { get; set; }


        #endregion Properties

        public ShellViewModel(IEventAggregator aggregator, ILocalizationHelper loc, DeviceListViewModel deviceListViewModel)
        {
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);

            // Adding Menu Items
            var menuItem = IoC.Get<MenuItemViewModel>();
            menuItem.Create(deviceListViewModel, null, PackIconKind.Chip, loc.GetByKey("Devices"));
            deviceListViewModel.Menu = menuItem;
            MenuItems.Add(menuItem);
            ActivateView(menuItem);
        }

        public Task HandleAsync(TreeViewSelectionChangedMessage message, CancellationToken cancellationToken)
        {
            if (message.SelectedItem.IsSelected)
            {
                SelectedMenuItem = message.SelectedItem;
                if (SelectedMenuItem.ViewModel is INotifyMeOnMenuSelect menuViewModel)
                {
                    menuViewModel.OnMenuSelect();
                }
                ActivateView(SelectedMenuItem);
            }

            return Task.CompletedTask;
        }

        private void ActivateView(MenuItemViewModel menu)
        {
            SelectedMenuItem = menu;
            ActivateItemAsync(SelectedMenuItem.ViewModel, new CancellationToken());
        }
    }
}
