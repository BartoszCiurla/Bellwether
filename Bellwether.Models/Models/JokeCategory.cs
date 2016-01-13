
namespace Bellwether.Models.Models
{
    public class JokeCategory
    {
        public int Id { get; set; }//this is global id not language translation
        public string JokeCategoryName { get; set; }
        public int LanguageId { get; set; }
    }
}
