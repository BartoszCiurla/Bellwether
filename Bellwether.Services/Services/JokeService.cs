﻿//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Bellwether.Models.ViewModels;
//using Bellwether.Repositories.Entities;
//using Bellwether.Repositories.Factories;
//using Bellwether.Repositories.Repositories;
//using Bellwether.Services.Factories;
//using Bellwether.Services.WebServices;

//namespace Bellwether.Services.Services
//{
//    public interface IJokeService
//    {
//        IEnumerable<JokeViewModel> GetLocalJokes();
//        IEnumerable<JokeCategoryDao> GetLocalJokeCategories();
//        Task<bool> CheckAndFillJokeCategories(string urlWithParams);
//        Task<bool> CheckAndFillJokes(string urlWithParams);

//    }
//    public class JokeService:IJokeService
//    {
//        private readonly IJokeRepository _jokeRepository;
//        private readonly IJokeCategoryRepository _jokeCategoryRepository;
//        private readonly IWebBellwetherJokeService _jokeService;

//        public JokeService()
//        {
//            _jokeRepository = OldRepositoryFactory.JokeRepository;
//            _jokeCategoryRepository = OldRepositoryFactory.JokeCategoryRepository;
//            _jokeService = ServiceFactory.WebBellwetherJokeService;
//        }

//        public IEnumerable<JokeViewModel> GetLocalJokes()
//        {
//            return _jokeRepository.GetJokes();
//        }

//        public IEnumerable<JokeCategoryDao> GetLocalJokeCategories()
//        {
//            return _jokeCategoryRepository.GetJokeCategories();
//        }
//        public async Task<bool> CheckAndFillJokeCategories(string urlWithParams)
//        {
//            var mandatoryJokeCategories = await _jokeService.GetJokeCategories(urlWithParams);
//            return  _jokeCategoryRepository.CheckAndFillJokeCategories(mandatoryJokeCategories);
//        }

//        public async Task<bool> CheckAndFillJokes(string urlWithParams)
//        {
//            var mandatoryJokes = await _jokeService.GetJokes(urlWithParams);
//            return _jokeRepository.CheckAndFillJokes(mandatoryJokes);
//        }

//    }
//}