namespace Bellwether.Models.ViewModels
{
    public class JokeViewModel
    {
        public int Id { get; set; }
        public string JokeContent { get; set; }
        public int JokeCategoryId { get; set; }
        public string JokeCategoryName { get; set; }
    }
}
