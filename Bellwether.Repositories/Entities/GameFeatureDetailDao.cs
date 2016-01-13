namespace Bellwether.Repositories.Entities
{
    public class GameFeatureDetailDao
    {
        public int Id { get; set; }
        public string GameFeatureDetailName { get; set; }
        public virtual BellwetherLanguageDao Language { get; set; }
    }
}
