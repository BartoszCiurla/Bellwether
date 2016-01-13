namespace Bellwether.Models.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public string JokeContent { get; set; }
        public int JokeCategoryId { get; set; }
        public int LanguageId { get; set; }
    }
}
