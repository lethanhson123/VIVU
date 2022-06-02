using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Model;

namespace VIVU.Logic.Queries.Implements
{
    public class BannerQueries : IBannerQueries
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public BannerQueries(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public IEnumerable<BannerModel> Get(BannerQueryModel query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BannerModel> Get(string? keywords)
        {
            return database.Banners.Where(x => x.IsDeleted != true &&
                                              (x.Name.Contains(keywords ?? string.Empty) ||
                                              x.Title.Contains(keywords ?? string.Empty) ||
                                              x.SubTitle.Contains(keywords ?? string.Empty)))
               .Select(x => mapper.Map<BannerModel>(x));
        }

        public Task<BannerModel> GetDetail(int Id)
        {
            var data = new BannerModel();

            var hexagram = database.Banners.FirstOrDefault(x => x.Id == Id &&
                                                              x.IsDeleted != true);
            if (hexagram != null)
            {
                mapper.Map(hexagram, data);
            }

            return Task.FromResult(data);
        }
    }
}
