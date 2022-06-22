using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Logic.Queries.Implements
{
    public class MarketLeadQueries :IMarketLeadQueries
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public MarketLeadQueries(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }
        public IEnumerable<MarketLeadModel> Get()
        {
            return database.MarketLeads.Where(x => x.IsDeleted != true)
             .Select(x => mapper.Map<MarketLeadModel>(x));
        }
        public IEnumerable<MarketLeadModel> Get(MarketLeadQueryModel query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MarketLeadModel> Get(string? keywords)
        {
            return database.MarketLeads.Where(x => x.IsDeleted != true &&
                                              (x.Name.Contains(keywords ?? string.Empty) ||
                                              x.Phone.Contains(keywords ?? string.Empty) ||
                                              x.Email.Contains(keywords ?? string.Empty) ||
                                              x.Channel.Contains(keywords ?? string.Empty)))
               .Select(x => mapper.Map<MarketLeadModel>(x));
        }

        public Task<MarketLeadModel> GetDetail(int Id)
        {
            var data = new MarketLeadModel();

            var hexagram = database.MarketLeads.FirstOrDefault(x => x.Id == Id &&
                                                              x.IsDeleted != true);
            if (hexagram != null)
            {
                mapper.Map(hexagram, data);
            }

            return Task.FromResult(data);
        }
    }
}
