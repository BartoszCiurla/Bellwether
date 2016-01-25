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

namespace Bellwether.Views.Joke
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class JokePage : Page
    {
        private static double _persistedItemContainerHeight = -1;
        private static string _persistedItemKey = "";
        private static string _persistedPosition = "";
        private readonly JokePageViewModel _viewModel = new JokePageViewModel();
        public JokePage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                this.DataContext = _viewModel;
                OnLoaded(s, e);
                LoadScrolPosition(s, e);
            };
        }
        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            _viewModel.SelectedJoke = e.ClickedItem as JokeViewModel;
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(JokeDetailPage), _viewModel, new DrillInNavigationTransitionInfo());
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedJoke == null && _viewModel.Jokes.Count > 0)
            {
                _viewModel.SelectedJoke = _viewModel.Jokes[0];
                JokeListView.SelectedIndex = 0;
            }
            // If the app starts in narrow mode - showing only the Master listView - 
            // it is necessary to set the commands and the selection mode.
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                VisualStateManager.GoToState(this, NarrowState.Name, true);
            }
            else if (PageSizeStatesGroup.CurrentState == WideState)
            {
                // In this case, the app starts is wide mode, Master/Details view, 
                // so it is necessary to set the commands and the selection mode.
                VisualStateManager.GoToState(this, WideState.Name, true);
                JokeListView.SelectionMode = ListViewSelectionMode.Single;
                JokeListView.SelectedItem = _viewModel.SelectedJoke;
                int indexOfSelectedGame = _viewModel.Jokes.IndexOf(_viewModel.SelectedJoke);
                JokeListView.SelectedIndex = indexOfSelectedGame;
            }
            else
            {
                new InvalidOperationException();
            }
        }

        private async void LoadScrolPosition(object sender, RoutedEventArgs e)
        {      
            if (!string.IsNullOrEmpty(_persistedPosition))            
                await ListViewPersistenceHelper.SetRelativeScrollPositionAsync(this.JokeListView, _persistedPosition, this.GetItem);            
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            try
            {
                _persistedPosition = ListViewPersistenceHelper.GetRelativeScrollPosition(this.JokeListView, this.GetKey);
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
            var singleItem = item as JokeViewModel;
            if (singleItem != null)
            {
                _persistedItemContainerHeight = (JokeListView.ContainerFromItem(item) as ListViewItem).ActualHeight;
                _persistedItemKey = singleItem.Id.ToString();
                return _persistedItemKey;
            }
            return string.Empty;
        }
        private IAsyncOperation<object> GetItem(string key)
        {
            // This function takes a key that ListViewPersistenceHelper parsed out of the _persistedPosition string
            // It returns an item that corresponds to the given key
            // The implementation of this function is dependent on the data model you are using. Every item in your list should have
            // a unique key this function returns
            return Task.Run(() =>
            {
                if (_viewModel.Jokes.Count <= 0)
                {
                    return null;
                }
                return (object)_viewModel.Jokes.FirstOrDefault(i => i.Id == Convert.ToInt32(key));
            }).AsAsyncOperation();
        }

        private void JokeListView_OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            // This function manually sets the height of the item ListViewPersistenceHelper is attempting to scroll to. We need to set the height
            // because if the item is not fully rendered at the time of scrolling, it can return an incorrect height and cause ListViewPersistenceHelper 
            // to overscroll. 
            var singleItem = args.Item as JokeViewModel;
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
        private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            bool isNarrow = e.NewState == NarrowState;
            if (isNarrow)
            {
                Frame.Navigate(typeof(JokeDetailPage), _viewModel, new SuppressNavigationTransitionInfo());
            }
            else
            {
                //VisualStateManager.GoToState(this, WideState.Name, true);
                //JokeListView.SelectionMode = ListViewSelectionMode.Single;
                JokeListView.SelectedItem = _viewModel.SelectedJoke;
                int indexOfSelectedGame = _viewModel.Jokes.IndexOf(_viewModel.SelectedJoke);
                JokeListView.SelectedIndex = indexOfSelectedGame;
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(JokeListView, isNarrow);
            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableContentTransitions();
        }
        private void EnableContentTransitions()
        {
            DetailContentPresenter.ChildrenTransitions.Clear();
            DetailContentPresenter.ChildrenTransitions.Add(new EntranceThemeTransition { FromHorizontalOffset = 500, FromVerticalOffset = 500 });
        }
    }
}
