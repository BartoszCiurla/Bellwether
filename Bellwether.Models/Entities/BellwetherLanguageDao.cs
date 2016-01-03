﻿using System.ComponentModel.DataAnnotations;

namespace Bellwether.Models.Entities
{
    public class BellwetherLanguageDao
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public string LanguageShortName { get; set; }
    }
}
