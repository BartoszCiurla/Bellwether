namespace Bellwether.Repositories.Entities
{
    public class JokeCategoryDao
    {
        public int Id { get; set; }//this is global id not language translation
        public string JokeCategoryName { get; set; }
        public virtual BellwetherLanguageDao Language { get; set; }
    }
}
