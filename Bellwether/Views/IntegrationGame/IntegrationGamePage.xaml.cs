using System;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Bellwether.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bellwether.Views.IntegrationGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IntegrationGamePage : Page
    {
        private IntegrationGameViewModel _viewModel;
        public IntegrationGamePage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                _viewModel = new IntegrationGameViewModel();
                this.DataContext = _viewModel;
                MasterListView.ItemsSource = _viewModel.IntegrationGames;
                OnLoaded(s, e);
            };
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            // The clicked item it is the new selected contact
            _viewModel.SelectedIntegrationGame = e.ClickedItem as Models.ViewModels.IntegrationGameViewModel;
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                // Go to the details page and display the item 
                Frame.Navigate(typeof(IntegrationGameDetailPage), _viewModel.SelectedIntegrationGame, new DrillInNavigationTransitionInfo());
            }
            //else
            {
                // Play a refresh animation when the user switches detail items.
                //EnableContentTransitions();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedIntegrationGame == null && _viewModel.IntegrationGames.Count > 0)
            {
                _viewModel.SelectedIntegrationGame = _viewModel.IntegrationGames[0];
                MasterListView.SelectedIndex = 0;
            }
            // If the app starts in narrow mode - showing only the Master listView - 
            // it is necessary to set the commands and the selection mode.
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                VisualStateManager.GoToState(this, MasterState.Name, true);
            }
            else if (PageSizeStatesGroup.CurrentState == WideState)
            {
                // In this case, the app starts is wide mode, Master/Details view, 
                // so it is necessary to set the commands and the selection mode.
                VisualStateManager.GoToState(this, MasterDetailsState.Name, true);
                MasterListView.SelectionMode = ListViewSelectionMode.Extended;
                MasterListView.SelectedItem = _viewModel.SelectedIntegrationGame;
            }
            else
            {
                new InvalidOperationException();
            }
        }

        private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            bool isNarrow = e.NewState == NarrowState;
            if (isNarrow)
            {
                Frame.Navigate(typeof(IntegrationGameDetailPage), _viewModel.SelectedIntegrationGame, new SuppressNavigationTransitionInfo());
            }
            else
            {
                //VisualStateManager.GoToState(this, MasterDetailsState.Name, true);
                MasterListView.SelectionMode = ListViewSelectionMode.Extended;
                MasterListView.SelectedItem = _viewModel.SelectedIntegrationGame;
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);
            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
        }


        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageSizeStatesGroup.CurrentState == WideState)
            {
                if (MasterListView.SelectedItems.Count == 1)
                {
                    _viewModel.SelectedIntegrationGame = MasterListView.SelectedItem as Models.ViewModels.IntegrationGameViewModel;
                    EnableContentTransitions();
                }
                // Entering in Extended selection
                else if (MasterListView.SelectedItems.Count > 1
                     && MasterDetailsStatesGroup.CurrentState == MasterDetailsState)
                {
                    VisualStateManager.GoToState(this, ExtendedSelectionState.Name, true);
                }
            }
            // Exiting Extended selection
            if (MasterDetailsStatesGroup.CurrentState == ExtendedSelectionState &&
                MasterListView.SelectedItems.Count == 1)
            {
                VisualStateManager.GoToState(this, MasterDetailsState.Name, true);
            }
        }
        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }
    }
}
