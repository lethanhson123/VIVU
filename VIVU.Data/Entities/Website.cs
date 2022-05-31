using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    public class Website
    {
        public int Id { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string ImageWidth { get; set; } = string.Empty;
        public string ImageHeight { get; set; } = string.Empty;

        /// <summary>
        /// Meta for site
        /// </summary>
        public string Medium { get; set; } = string.Empty;
        public string InLanguage { get; set; } = string.Empty;
        public string ArticleSection { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Copyright { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Robots { get; set; } = string.Empty;
        public string GeoPlaceName { get; set; } = string.Empty;
        public string GeoRegion { get; set; } = string.Empty;
        public string GeoPosition { get; set; } = string.Empty;
        public string? ICBM { get; set; } = string.Empty;
        public string? RevisitAfter { get; set; } = string.Empty;
    }
}
