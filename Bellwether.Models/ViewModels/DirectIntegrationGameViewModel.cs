namespace Bellwether.Models.ViewModels
{
    public class DirectIntegrationGameViewModel
    {
        public int Id { get; set; }
        public int IntegrationGameId { get; set; }
        public string GameName { get; set; }
        public string GameDescription { get; set; }
        public string CategoryGame { get; set; }
        public string PaceOfPlay { get; set; }
        public string NumberOfPlayer { get; set; }
        public string PreparationFun { get; set; }
        public int LanguageId { get; set; }
    }
}
