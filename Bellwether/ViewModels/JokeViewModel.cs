
using System.Threading.Tasks;
using Bellwether.Services.Utility;
using Bellwether.Utility;

namespace Bellwether.ViewModels
{
    public class JokeViewModel
    {
        public JokeViewModel()
        {
        }

        public async void Dupa()
        {
            await ServiceExecutor.ExecuteAsync(() => ServiceFactory.InitResourceService.Initiate());
        }
        //private readonly IJokeService _jokeService;
        //private IResourceService _resourceService;
        //public JokeViewModel()
        //{
        //    _jokeService = ServiceFactory.JokeService;
        //    _resourceService = ServiceFactory.ResourceService;
        //    InitTest();
        //}
        //public ObservableCollection<JokeCategoryDao> JokeCategories { get; set; }
        //public ObservableCollection<Models.ViewModels.JokeViewModel> Jokes { get; set; }
        //public void InitTest()
        //{
            
        //    JokeCategories = new ObservableCollection<JokeCategoryDao>(_jokeService.GetLocalJokeCategories());
        //    Jokes = new ObservableCollection<Models.ViewModels.JokeViewModel>(_jokeService.GetLocalJokes());
        //}

        //public async void Dupa()
        //{
        //   var test = await ServiceExecutor.ExecuteAsync(() => ServiceFactory.InitResourceService.Init());
        //}
    }
}
