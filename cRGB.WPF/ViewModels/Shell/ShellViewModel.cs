using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.Domain.Services;
using cRGB.Domain.Services.System;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Navigation;
using MaterialDesignThemes.Wpf;
using Screen = Caliburn.Micro.Screen;

namespace cRGB.WPF.ViewModels.Shell
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<TreeViewSelectionChangedMessage>, IDisposable
    {
        readonly IEventAggregator _eventAggregator;
        readonly ISettingsService _settingsService;
        readonly IMenuItemViewModelFactory _menuFactory;

        #region Properties

        /// <summary>
        /// All Menu Items are declared here. (Left side navigation bar)
        /// </summary>
        public BindableCollection<IMenuItemViewModel> MenuItems { get; } = new BindableCollection<IMenuItemViewModel>();

        public IMenuItemViewModel SelectedMenuItem { get; set; }

        public int ShellViewHeight
        {
            get => _settingsService.Settings.AppSettings.ShellViewHeight;
            set => _settingsService.Settings.AppSettings.ShellViewHeight = value;
        }

        public int ShellViewWidth
        {
            get => _settingsService.Settings.AppSettings.ShellViewWidth;
            set => _settingsService.Settings.AppSettings.ShellViewWidth = value;
        }


        #endregion Properties

        public ShellViewModel(IEventAggregator aggregator, ILocalizationHelper loc, IMenuItemViewModelFactory menuFactory, ISettingsService settingsService, DeviceListViewModel deviceListViewModel)
        {
            _eventAggregator = aggregator;
            _settingsService = settingsService;
            _menuFactory = menuFactory;
            _eventAggregator.SubscribeOnUIThread(this);

            // Adding Menu Items
            var menuItem = menuFactory.Create();
            menuItem.CreateMenu(deviceListViewModel, null, PackIconKind.Chip, loc.GetByKey("Devices"));
            deviceListViewModel.Menu = menuItem;
            MenuItems.Add(menuItem);
            ActivateView(menuItem);
            deviceListViewModel.Init();
        }

        public Task HandleAsync(TreeViewSelectionChangedMessage message, CancellationToken cancellationToken)
        {
            if (message.SelectedItem.IsSelected)
            {
                SelectedMenuItem = message.SelectedItem;
                var menuViewModel = SelectedMenuItem.ViewModel as INotifyMeOnMenuSelect;
                menuViewModel?.OnMenuSelect();

                ActivateView(SelectedMenuItem);
            }

            return Task.CompletedTask;
        }

        private void ActivateView(IMenuItemViewModel menu)
        {
            SelectedMenuItem = menu;
            ActivateItemAsync((Screen)SelectedMenuItem.ViewModel, new CancellationToken());
        }

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}
