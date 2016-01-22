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
        private IntegrationGamePageViewModel _viewModel;
        public IntegrationGamePage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                _viewModel = new IntegrationGamePageViewModel();             
                this.DataContext = _viewModel;
                OnLoaded(s, e);
            };
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            _viewModel.SelectedIntegrationGame = e.ClickedItem as Models.ViewModels.IntegrationGameViewModel;
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(IntegrationGameDetailPage), _viewModel, new DrillInNavigationTransitionInfo());
            }

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            MasterListView.SelectedItem = _viewModel.SelectedIntegrationGame;           
        }

        private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            bool isNarrow = e.NewState == NarrowState;
            if (isNarrow)
            {
                Frame.Navigate(typeof(IntegrationGameDetailPage), _viewModel, new SuppressNavigationTransitionInfo());
            }
            else
            {
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
            //if (PageSizeStatesGroup.CurrentState == WideState)
            //{
            //    if (MasterListView.SelectedItems.Count == 1)
            //    {
            //        _viewModel.SelectedIntegrationGame = MasterListView.SelectedItem as Models.ViewModels.IntegrationGameViewModel;
                    EnableContentTransitions();
            //    }         
            //}
        }
        private void EnableContentTransitions()
        {
            //DetailContentPresenter.ContentTransitions.Clear();
            
            DetailContentPresenter.ChildrenTransitions.Clear();
            DetailContentPresenter.ChildrenTransitions.Add(new EntranceThemeTransition { FromHorizontalOffset = 500, FromVerticalOffset = 500 });
            //DetailContentPresenter.ChildrenTransitions.Add(new EntranceThemeTransition());
           // DetailContentPresenter.ChildrenTransitions.Add(new PopupThemeTransition {FromHorizontalOffset = 500,FromVerticalOffset = 500});
           //DetailContentPresenter.ChildrenTransitions.Add();
        }
    }
}
