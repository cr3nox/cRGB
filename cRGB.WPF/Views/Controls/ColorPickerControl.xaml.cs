using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Caliburn.Micro;
using Serilog;

namespace cRGB.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickerControl.xaml
    /// </summary>
    public partial class ColorPickerControl : UserControl
    {
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value); 
        }

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color),
                typeof(ColorPickerControl), new PropertyMetadata());

        public ColorPickerControl()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        void RandomButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rng = new Random();
            SelectedColor = Color.FromRgb((byte)rng.Next(0, 255), (byte)rng.Next(0, 255), (byte)rng.Next(0, 255));
            
        }
    }
}
