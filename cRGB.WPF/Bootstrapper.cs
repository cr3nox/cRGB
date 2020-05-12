using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using cRGB.Domain.Services;
using cRGB.Domain.Services.Device;
using cRGB.Domain.Services.System;
using cRGB.Modules.Audio;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.Extensions;
using cRGB.WPF.Helpers;
using cRGB.WPF.ViewModels.Controls;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Event;
using cRGB.WPF.ViewModels.Event.Events;
using cRGB.WPF.ViewModels.Menu;
using cRGB.WPF.ViewModels.Shell;
using Serilog;
using Serilog.Events;

namespace cRGB.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        //private SimpleContainer _container;
        private WindsorContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            LogManager.GetLog = type => new DebugLog(type);

            _container = new WindsorContainer();
            
            _container.Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton());
            _container.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton());
            _container.Register(Component.For<ShellViewModel>().ImplementedBy<ShellViewModel>().LifestyleSingleton());

            // Helpers
            _container.Register(Component.For<ILocalizationHelper>().ImplementedBy<LocalizationHelper>().LifestyleSingleton());

            // Services
            _container.Register(Component.For<ILogService>().ImplementedBy<LogService>().LifestyleSingleton());
            _container.Register(Component.For<IXmlSerializationService>().ImplementedBy<XmlSerializationService>().LifestyleSingleton());
            _container.Register(Component.For<IBlinkStickService>().ImplementedBy<BlinkStickService>().LifestyleSingleton());
            _container.Register(Component.For<ISettingsService>().ImplementedBy<SettingsService>().LifestyleSingleton());

            // ViewModels Transient
            _container.Register(Component.For<MenuItemViewModel>().ImplementedBy<MenuItemViewModel>().LifestyleTransient());
            _container.Register(Component.For<BlinkStickViewModel>().ImplementedBy<BlinkStickViewModel>().LifestyleTransient());
            _container.Register(Component.For<DeviceSelectionViewModel>().ImplementedBy<DeviceSelectionViewModel>().LifestyleTransient());
            _container.Register(Component.For<BlinkStickSettingsViewModel>().ImplementedBy<BlinkStickSettingsViewModel>().LifestyleTransient());
            _container.Register(Component.For<LedViewModel>().ImplementedBy<LedViewModel>().LifestyleTransient());
            // ViewModels Singleton
            _container.Register(Component.For<DeviceListViewModel>().ImplementedBy<DeviceListViewModel>().LifestyleSingleton());
            _container.Register(Component.For<IEventListViewModel>().ImplementedBy<EventListViewModel>().LifestyleSingleton());
            // ViewModel Controls
            _container.Register(Component.For<DialogComboBoxSelectionViewModel>().ImplementedBy<DialogComboBoxSelectionViewModel>().LifestyleTransient());

            // EventViewModels
            // Register All Classes that are IEventViewModel
            _container.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn(typeof(IEventViewModel))
                    .WithService.Base()
                    .LifestyleTransient());

            // cRGB.Modules.Audio Module uses cscore which only runs on .NET Framework. Can only run on Windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _container.Install(new AudioInstaller());
            }
        }

        private void StartLogger()
        {
            Log.Logger = _container.Resolve<ILogService>().InitLogger();
            Log.Information("Application Start");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            StartLogger();
            var settings = _container.Resolve<ISettingsService>();
            settings.LoadAll();
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                ? _container.Kernel.HasComponent(service)
                    ? _container.Resolve(service)
                    : base.GetInstance(service, key)
                : _container.Kernel.HasComponent(key)
                    ? _container.Resolve(key, service)
                    : base.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            // Does not work properly so just do direct cast, even resharper considers it suspicious
            //return _container.ResolveAll(service).OfType<Array>().ToList();
            return _container.ResolveAll(service) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            instance.GetType().GetProperties()
                .Where(property => property.CanWrite && property.PropertyType.IsPublic)
                .Where(property => _container.Kernel.HasComponent(property.PropertyType))
                .ForEach(property => property.SetValue(instance, _container.Resolve(property.PropertyType), null));
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
            var shell = _container.Resolve<ShellViewModel>();
            RecursiveViewModelOnExit(shell.MenuItems);

            var settings = _container.Resolve<ISettingsService>();
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
