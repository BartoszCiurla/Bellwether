namespace Bellwether.Models.ViewModels
{
    public class SimpleIntegrationGameViewModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string GameDescription { get; set; }
        public int[] GameFeatures { get; set; }
        public int LanguageId { get; set; }
    }
}
