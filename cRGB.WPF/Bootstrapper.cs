using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using cRGB.Domain;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Services.Device;
using cRGB.Domain.Services.System;
using cRGB.Modules.Audio.Windows;
using cRGB.Tools.Helpers;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ServiceLocation.Selectors;
using cRGB.WPF.ViewModels.Controls;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Effect.Effects;
using cRGB.WPF.ViewModels.Event;
using cRGB.WPF.ViewModels.Navigation;
using cRGB.WPF.ViewModels.Shell;
using Serilog;

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

            // Disable property injection in castle.windsor kernel
            _container.Kernel.ComponentModelBuilder.RemoveContributor(_container.Kernel.ComponentModelBuilder
                .Contributors
                .OfType<PropertiesDependenciesModelInspector>()
                .Single());

            _container.Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton());
            _container.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton());
            _container.Register(Component.For<ShellViewModel>().ImplementedBy<ShellViewModel>().LifestyleSingleton());

            // Factories and Facilities
            // used for creating instances without Service Locator
            _container.AddFacility<TypedFactoryFacility>();
            _container.Register(Component.For<IEventViewModelFactory>().AsFactory(f => f.SelectedWith(new ClassByNameComponentSelector())));
            _container.Register(Component.For<IEffectViewModelFactory>().AsFactory(f => f.SelectedWith(new ClassByNameComponentSelector())));
            _container.Register(Component.For<IMessageFactory>().AsFactory(f => f.SelectedWith(new ClassByNameComponentSelector())));

            // Models
            _container.Install(
                new DomainInstaller()
            );
            _container.Register(Component.For<ILedEventFactory>().AsFactory(f => f.SelectedWith(new ClassByNameComponentSelector())));
            _container.Register(Component.For<ILedEffectFactory>().AsFactory(f => f.SelectedWith(new ClassByNameComponentSelector())));

            // Helpers
            _container.Register(Component.For<ILocalizationHelper>().ImplementedBy<LocalizationHelper>().LifestyleSingleton());

            // TODO: Add service interface for each viewmodel
            // ViewModels Transient
            _container.Register(Component.For<IMenuItemViewModel>().ImplementedBy<MenuItemViewModel>().LifestyleTransient(), Component.For<IMenuItemViewModelFactory>().AsFactory());
            _container.Register(Component.For<BlinkStickViewModel>().ImplementedBy<BlinkStickViewModel>().LifestyleTransient(), Component.For<IBlinkStickViewModelFactory>().AsFactory());
            _container.Register(Component.For<BlinkStickSettingsViewModel>().ImplementedBy<BlinkStickSettingsViewModel>().LifestyleTransient(), Component.For<IBlinkStickSettingsViewModelFactory>().AsFactory());
            _container.Register(Component.For<ILedViewModel>().ImplementedBy<LedViewModel>().LifestyleTransient(), Component.For<ILedViewModelFactory>().AsFactory());
            _container.Register(Component.For<DeviceSelectionViewModel>().ImplementedBy<DeviceSelectionViewModel>().LifestyleTransient());
            // ViewModels Singleton
            _container.Register(Component.For<DeviceListViewModel>().ImplementedBy<DeviceListViewModel>().LifestyleSingleton());
            _container.Register(Component.For<IEffectViewModel>().ImplementedBy<EffectViewModel>().LifestyleSingleton());
            _container.Register(Component.For<IEventListViewModel>().ImplementedBy<EventListViewModel>().LifestyleSingleton());
            // ViewModel Controls
            _container.Register(Component.For<IDialogComboBoxSelectionViewModel>().ImplementedBy<DialogComboBoxSelectionViewModel>().LifestyleTransient());

            // EventViewModels
            // Register All Classes that are IEventViewModel
            _container.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn(typeof(IEventViewModel))
                    .WithService.Base()
                    .Configure(c => c.Named(c.Implementation.Name))
                    .LifestyleTransient());
            
            // Messages
            _container.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn(typeof(IMessage))
                    .WithService.Base()
                    .Configure(c => c.Named(c.Implementation.Name))
                    .LifestyleTransient());

            // Windows Only Modules
            if (OS.IsWindows())
            {
                // cRGB.Modules.Audio Module uses cscore which only runs on .NET Framework. Can only run on Windows
                _container.Install(new WindowsAudioModuleInstaller());
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
            // Does not work properly so just do direct cast even resharper considers it suspicious
            //return _container.ResolveAll(service).OfType<Array>().ToList();
            return (IEnumerable<object>) _container.ResolveAll(service);
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

        protected void RecursiveViewModelOnExit(BindableCollection<IMenuItemViewModel> menus)
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
