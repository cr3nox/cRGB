using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using cRGB.Domain;
using cRGB.WPF.Helpers;
using cRGB.WPF.ViewModels;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Menu;
using cRGB.WPF.ViewModels.Shell;

namespace cRGB.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            LogManager.GetLog = type => new DebugLog(type);

            _container = new SimpleContainer();

            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            _container
                .PerRequest<ShellViewModel>();

            // Helpers
            _container.Singleton<ILocalizationHelper, LocalizationHelper>();
            
            // ViewModels
            _container.PerRequest<MenuItemViewModel>();
            _container.PerRequest<DeviceListViewModel>();
            _container.Singleton<BlinkStickViewModel>();
            _container.PerRequest<DeviceSelectionViewModel>();
            _container.PerRequest<BlinkStickSettingsViewModel>();
            //old
            _container.Singleton<MenuItemOverviewViewModel>();

            // Controllers
            _container.Singleton<BlinkStickService>();
            
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message, "An error as occurred", MessageBoxButton.OK);
        }
    }
}
