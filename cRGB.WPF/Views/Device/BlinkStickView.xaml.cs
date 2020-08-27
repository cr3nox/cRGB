using System;
using System.Threading;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using cRGB.Tools.Interfaces.View;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.ViewModels.Device;

namespace cRGB.WPF.Views.Device
{
    /// <summary>
    /// Interaktionslogik für BlinkStickSettings.xaml
    /// </summary>
    public partial class BlinkStickView : UserControl
    {
        DateTime lastRefresh;
        public BlinkStickView()
        {
            InitializeComponent();
            lastRefresh = DateTime.Now;
            CompositionTarget.Rendering += OnRendering;
        }

        public void OnRendering(object sender, EventArgs e)
        {
            // only refresh every few ms
            if (DateTime.Now.Subtract(lastRefresh).Milliseconds < 50)
                return;

            if (DataContext is IRefresh refresh)
                refresh.RefreshProperties();
            lastRefresh = DateTime.Now;
        }
    }
}
