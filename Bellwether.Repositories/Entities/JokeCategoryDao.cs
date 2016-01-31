namespace Bellwether.Repositories.Entities
{
    public class JokeCategoryDao
    {
        public int Id { get; set; }//this is global id not language translation
        public string JokeCategoryName { get; set; }
        public int LanguageId { get; set; }
        public BellwetherLanguageDao Language { get; set; }
    }
}
