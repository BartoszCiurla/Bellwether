using AutoMapper;
using Bellwether.Models.Models;
using Bellwether.Repositories.Entities;

namespace Bellwether.Services.Config
{
    public class AutoMapperConfig
    {
        public AutoMapperConfig()
        {
            Mapper.CreateMap<BellwetherLanguage, BellwetherLanguageDao>();
            Mapper.CreateMap<BellwetherLanguageDao, BellwetherLanguage>();

            Mapper.CreateMap<JokeCategory, JokeCategoryDao>();
            Mapper.CreateMap<JokeCategoryDao, JokeCategory>();

            Mapper.CreateMap<Joke, JokeDao>();
            Mapper.CreateMap<JokeDao, Joke>();
        }
    }
}
