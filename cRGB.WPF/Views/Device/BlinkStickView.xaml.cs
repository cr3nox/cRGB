using System;
using System.Windows.Controls;
using System.Windows.Media;
using cRGB.Tools.Interfaces.View;
using cRGB.Tools.Interfaces.ViewModel;

namespace cRGB.WPF.Views.Device
{
    /// <summary>
    /// Interaktionslogik für BlinkStickSettings.xaml
    /// </summary>
    public partial class BlinkStickView : UserControl, IRefreshOnRenderingFrame
    {
        public BlinkStickView()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRendering;
        }

        public void OnRendering(object sender, EventArgs e)
        {
            if (DataContext is IRefresh refresh)
                refresh.RefreshProperties();
        }
    }
}
