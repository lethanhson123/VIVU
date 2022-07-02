using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Model;

namespace VIVU.Logic.Queries.Implements
{
    public class TagQueries : ITagQueries
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public TagQueries(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public IEnumerable<TagModel> Get(TagQueryModel query)
        {
            return database.Tags.Where(x => x.IsDeleted != true &&
                                               (x.Name.Contains(query.Keywords ?? string.Empty) ||
                                               x.Title.Contains(query.Keywords ?? string.Empty) ||
                                               x.Description.Contains(query.Keywords ?? string.Empty)
                                               )
                                             )
                .Select(x => mapper.Map<TagModel>(x)).Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);
        }

        public IEnumerable<TagModel> Get()
        {
            return database.Tags
                .Select(x => mapper.Map<TagModel>(x));
        }

        public Task<TagDetailModel> GetDetail(int Id)
        {
            var data = new TagDetailModel();

            var tag = database.Tags.FirstOrDefault(x => x.Id == Id &&
                                                              x.IsDeleted != true);
            if (tag != null)
            {
                mapper.Map(tag, data);
                var postIds = database.PostTags
                    .Where(x => x.IsDeleted != true &&
                                x.TagId == tag.Id)
                    .Select(x => x.BlogId)
                    .ToList();
                data.Posts = database.Blogs
                    .Where(x => x.IsDeleted != true &&
                                postIds.Contains(x.Id))
                    .Select(x => mapper.Map<BlogModel>(x)).ToList();
            }

            return Task.FromResult(data);
        }
    }
}
