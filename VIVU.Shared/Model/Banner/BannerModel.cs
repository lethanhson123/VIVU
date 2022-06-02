﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class BannerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
