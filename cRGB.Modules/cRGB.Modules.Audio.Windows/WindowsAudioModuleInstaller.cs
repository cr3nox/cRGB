#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace cRGB.Modules.Audio.Windows
{
    public class WindowsAudioModuleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
        }
    }
}