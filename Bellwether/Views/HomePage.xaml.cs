using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bellwether.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private static DependencyProperty s_desiredHubSectionWidthProperty
            = DependencyProperty.Register("DesiredHubSectionWidth", typeof(double), typeof(HomePage), new PropertyMetadata(560.0));
        public HomePage()
        {
            this.InitializeComponent();
        }
        public static DependencyProperty DesiredHubSectionWidthProperty => s_desiredHubSectionWidthProperty;

        public double DesiredHubSectionWidth
        {
            get { return (double)GetValue(s_desiredHubSectionWidthProperty); }
            set { SetValue(s_desiredHubSectionWidthProperty, value); }
        }
        private void ParallaxBackgroundHub_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // For adaptability, we want to keep hub sections within one screen width.
            DesiredHubSectionWidth = e.NewSize.Width * .9;
        }
    }
}
