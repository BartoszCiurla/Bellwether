using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Bellwether.Models.ViewModels;
using Bellwether.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bellwether.Views.IntegrationGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IntegrationGamePage : Page
    {
        private static double _persistedItemContainerHeight = -1;
        private static string _persistedItemKey = "";
        private static string _persistedPosition = "";
        private IntegrationGamePageViewModel _viewModel;
        public IntegrationGamePage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                _viewModel = new IntegrationGamePageViewModel();
                this.DataContext = _viewModel;
                Page_Loaded(s, e);
            };
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            _viewModel.SelectedIntegrationGame = e.ClickedItem as IntegrationGameViewModel;
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(IntegrationGameDetailPage), _viewModel, new DrillInNavigationTransitionInfo());
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IntegrationGameListView.SelectedItem = _viewModel.SelectedIntegrationGame;
            if (!string.IsNullOrEmpty(_persistedPosition))
                await ListViewPersistenceHelper.SetRelativeScrollPositionAsync(this.IntegrationGameListView, _persistedPosition, this.GetItem);

        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            try
            {
                _persistedPosition = ListViewPersistenceHelper.GetRelativeScrollPosition(this.IntegrationGameListView, this.GetKey);
            }
            catch (Exception thisIsSparta)
            {

                Debug.WriteLine(thisIsSparta.Message + "it returns null but in fact operates interesting");
            }

            base.OnNavigatingFrom(e);
        }
        private string GetKey(object item)
        {
            // This function takes in the item at the top of the viewport at the moment of navigating away from the page, and returns
            // a key corresponding to that item. the implementation of this function is dependent on the data model you are using. In this 
            // function we also save the fully rendered _persistedItemContainerHeight. You do not need to do this if all of your items have 
            // the same fixed height. Finally, we save the key into a variable so it can be used outside of ListViewPersistenceHelper functions
            var singleItem = item as IntegrationGameViewModel;
            if (singleItem != null)
            {
                _persistedItemContainerHeight = (IntegrationGameListView.ContainerFromItem(item) as ListViewItem).ActualHeight;
                _persistedItemKey = singleItem.Id.ToString();
                return _persistedItemKey;
            }
            return string.Empty;
        }
        private void IntegrationGameListView_OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            // This function manually sets the height of the item ListViewPersistenceHelper is attempting to scroll to. We need to set the height
            // because if the item is not fully rendered at the time of scrolling, it can return an incorrect height and cause ListViewPersistenceHelper 
            // to overscroll. 
            var singleItem = args.Item as IntegrationGameViewModel;
            if (_persistedItemKey != "")
                if (singleItem != null && singleItem.Id == Convert.ToInt32(_persistedItemKey))
                {
                    if (!args.InRecycleQueue)
                    {
                        // Here we set the container's height equal to the fully rendered container height we had saved before navigating away. If all the items in 
                        // your list have the same fixed height, you can replace this variable with a hardcoded height value. 
                        args.ItemContainer.Height = _persistedItemContainerHeight;
                    }
                    else
                    {
                        // Containers in a list are recycled when they are scrolled out of view. However, if those containers have their Height property set and the content
                        // changes, that set Height is still applied. This creates an incorect UI if the items in your list are supposed to be of variable height. 
                        // If all the items in your list have the same fixed height, you do not have to do this. 
                        args.ItemContainer.ClearValue(HeightProperty);
                    }
                }
        }
        private IAsyncOperation<object> GetItem(string key)
        {
            // This function takes a key that ListViewPersistenceHelper parsed out of the _persistedPosition string
            // It returns an item that corresponds to the given key
            // The implementation of this function is dependent on the data model you are using. Every item in your list should have
            // a unique key this function returns
            return Task.Run(() =>
            {
                if (_viewModel.IntegrationGames.Count <= 0)
                {
                    return null;
                }
                return (object)_viewModel.IntegrationGames.FirstOrDefault(i => i.Id == Convert.ToInt32(key));
            }).AsAsyncOperation();
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
                IntegrationGameListView.SelectedItem = _viewModel.SelectedIntegrationGame;
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(IntegrationGameListView, isNarrow);
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
            DetailContentPresenter.ChildrenTransitions.Clear();
            DetailContentPresenter.ChildrenTransitions.Add(new EntranceThemeTransition { FromHorizontalOffset = 500, FromVerticalOffset = 500 });
        }


    }
}
