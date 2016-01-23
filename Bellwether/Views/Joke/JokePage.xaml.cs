using Windows.UI.Xaml.Controls;
using Bellwether.Models.ViewModels;
using Bellwether.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bellwether.Views.Joke
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class JokePage :Page
    {
        private JokePageViewModel _viewModel;
        public JokePage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                _viewModel = new JokePageViewModel();
                this.DataContext = _viewModel;
            };
        }

        private void ItemGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
               

        }

        private void ItemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            _viewModel.SelectedJoke = e.ClickedItem as JokeViewModel;
            _viewModel.Speak();
        }
    }
}
