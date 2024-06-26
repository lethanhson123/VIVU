﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
