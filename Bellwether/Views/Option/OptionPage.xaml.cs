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
        private OptionPageViewModel _viewModel;
        public OptionPage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                _viewModel = new OptionPageViewModel();
                this.DataContext = _viewModel;
                OnLoaded();
            };
        }

        private void OnLoaded()
        {
            int indexOfCurrentLanguage = _viewModel.Languages.IndexOf(_viewModel.CurrentLanguage);
            AvailableLanguages.SelectedIndex = indexOfCurrentLanguage;
            int indexOfCurrentVoice = _viewModel.Voices.IndexOf(_viewModel.CurrentVoice);
            VoiceLectors.SelectedIndex = indexOfCurrentVoice;           
        }
    }
}
