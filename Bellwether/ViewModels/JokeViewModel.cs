using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellwether.Models.Entities;
using Bellwether.Services.Factories;
using Bellwether.Services.Services;

namespace Bellwether.ViewModels
{
    public class JokeViewModel
    {
        private readonly IJokeService _jokeService;
        private IResourceService _resourceService;
        public JokeViewModel()
        {
            _jokeService = ServiceFactory.JokeService;
            _resourceService = ServiceFactory.ResourceService;
            InitTest();
        }
        public ObservableCollection<JokeCategoryDao> JokeCategories { get; set; }
        public ObservableCollection<Models.ViewModels.JokeViewModel> Jokes { get; set; }
        public void InitTest()
        {
            JokeCategories = new ObservableCollection<JokeCategoryDao>(_jokeService.GetLocalJokeCategories());
            Jokes = new ObservableCollection<Models.ViewModels.JokeViewModel>(_jokeService.GetLocalJokes());
        }
    }
}
