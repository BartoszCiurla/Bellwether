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
        }
    }
}
