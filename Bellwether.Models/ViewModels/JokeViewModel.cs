﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
