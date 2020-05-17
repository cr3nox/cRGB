#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.Event;
using cRGB.Domain.Services.Device;
using cRGB.Domain.Services.System;

namespace cRGB.Domain
{
    public class DomainInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILogService>().ImplementedBy<LogService>().LifestyleSingleton());
            container.Register(Component.For<IXmlSerializationService>().ImplementedBy<XmlSerializationService>().LifestyleSingleton());
            container.Register(Component.For<IBlinkStickService>().ImplementedBy<BlinkStickService>().LifestyleSingleton());
            container.Register(Component.For<ISettingsService>().ImplementedBy<SettingsService>().LifestyleSingleton());

            container.Register(Component.For<IBlinkStickSettings>().ImplementedBy<BlinkStickSettings>()
                .LifestyleTransient());

            container.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn(typeof(ILedEvent))
                    .WithService.DefaultInterfaces()
                    .Configure(c => c.Named(c.Implementation.Name))
                    .LifestyleTransient());
        }
    }
}