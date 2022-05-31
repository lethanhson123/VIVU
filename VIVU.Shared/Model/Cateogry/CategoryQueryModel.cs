﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model.Cateogry
{
    public class CategoryQueryModel
    {
        public string Keywords { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
