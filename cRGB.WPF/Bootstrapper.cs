using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using cRGB.Domain.Services;
using cRGB.Domain.Services.Device;
using cRGB.Domain.Services.System;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.Helpers;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Menu;
using cRGB.WPF.ViewModels.Shell;
using Serilog;
using Serilog.Events;

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
                .Singleton<ShellViewModel>();

            // Helpers
            _container.Singleton<ILocalizationHelper, LocalizationHelper>();
            
            // ViewModels
            _container.PerRequest<MenuItemViewModel>();
            _container.PerRequest<BlinkStickViewModel>();
            _container.PerRequest<DeviceSelectionViewModel>();
            _container.PerRequest<BlinkStickSettingsViewModel>();
            _container.PerRequest<LedViewModel>();

            _container.Singleton<DeviceListViewModel>();

            // Services
            _container.Singleton<IBlinkStickService, BlinkStickService>();
            //_container.Singleton<IJsonSerializationService, JsonSerializationService>();
            _container.Singleton<IXmlSerializationService, XmlSerializationService>();
            
            _container.Singleton<ISettingsService, SettingsService>();
            _container.Singleton<ILogService, LogService>();
            
        }

        private void StartLogger()
        {
            Log.Logger = _container.GetInstance<ILogService>().InitLogger();
            Log.Information("Application Start");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            StartLogger();
            var settings = _container.GetInstance<ISettingsService>();
            settings.LoadAll();
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
            Log.Fatal(e.Exception, $"OnUnhandledException, Sender: {sender}");
            e.Handled = true;
            MessageBox.Show(e.Exception.Message, "An fatal error as occurred", MessageBoxButton.OK);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            // Execute OnAppExit for every Viewmodel which inherits from INotifyMeOnAppExit
            var shell = _container.GetInstance<ShellViewModel>();
            RecursiveViewModelOnExit(shell.MenuItems);

            var settings = _container.GetInstance<ISettingsService>();
            settings.SaveAll();
            base.OnExit(sender, e);
        }

        protected void RecursiveViewModelOnExit(BindableCollection<MenuItemViewModel> menus)
        {
            foreach (var menu in menus)
            {
                if (menu.ViewModel is INotifyMeOnAppExit menuViewModel)
                {
                    menuViewModel.OnAppExit();
                }
                RecursiveViewModelOnExit(menu.Children);
            }
        }
    }
}
