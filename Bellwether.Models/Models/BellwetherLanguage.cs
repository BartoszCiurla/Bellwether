using System.ComponentModel.DataAnnotations;

namespace Bellwether.Models.Models
{
    public class BellwetherLanguage
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string LanguageName { get; set; }
        [Required]
        public string LanguageShortName { get; set; }
        [Required]
        public double LanguageVersion { get; set; }
    }
}
