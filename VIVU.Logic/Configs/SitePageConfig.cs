using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Logic.Configs
{
    public class SitePageConfig
    {
        public static string ConfigName { get; set; } = "SitePageConfig";
        public List<SitePage> SitePages { get; set; }
            = new List<SitePage>();

        public SitePage? GetByKey(string key)
        {
            SitePage? sitePage = SitePages.FirstOrDefault(x => x.Key == key);
            return sitePage;
        }
    }

    public class SitePage : HeaderModel
    {
        public string Key { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
    }
}
