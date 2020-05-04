using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using cRGB.Domain.Services;
using cRGB.WPF.Helpers;
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
            _container.PerRequest<BlinkStickViewModel>();
            _container.PerRequest<DeviceSelectionViewModel>();
            _container.PerRequest<BlinkStickSettingsViewModel>();
            _container.PerRequest<LedViewModel>();
            
            // Services
            _container.Singleton<IBlinkStickService, BlinkStickService>();
            _container.Singleton<IXmlSerializationService, XmlSerializationService>();
            _container.Singleton<IXmlSerializationService, XmlSerializationService>();
            
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

        protected override void OnExit(object sender, EventArgs e)
        {

            base.OnExit(sender, e);
        }
    }
}
