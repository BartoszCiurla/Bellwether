namespace Bellwether.Repositories.Entities
{
    public class JokeDao
    {
        public int Id { get; set; }//this is global id not id for language translation , i don't need language translation for joke in client app
        public string JokeContent { get; set; }
        public virtual JokeCategoryDao JokeCategory { get; set; }
        public virtual BellwetherLanguageDao Language { get; set; }
    }
}
