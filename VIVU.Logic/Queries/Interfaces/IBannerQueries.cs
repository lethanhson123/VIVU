using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Model;

namespace VIVU.Logic.Queries.Interfaces
{
    public interface IBannerQueries
    {
        IEnumerable<BannerModel> Get(BannerQueryModel query);
        IEnumerable<BannerModel> Get(string? keywords);
        Task<BannerModel> GetDetail(int Id);
    }
}
