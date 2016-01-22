using Windows.UI.Xaml.Controls;
using Bellwether.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bellwether.Views.Option
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OptionPage : Page
    {
        public OptionPage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                this.DataContext = new OptionPageViewModel();
            };
        }
    }
}
