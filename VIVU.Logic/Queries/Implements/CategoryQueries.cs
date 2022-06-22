using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Model;

namespace VIVU.Logic.Queries.Implements
{
    public class CategoriesQueries : ICategoryQueries
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public CategoriesQueries(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public IEnumerable<CategoryModel> Get(CategoryQueryModel query)
        {
            return database.Categories.Where(x => x.IsDeleted != true &&
                                               (x.Name.Contains(query.Keywords ?? string.Empty) ||
                                               x.Title.Contains(query.Keywords ?? string.Empty) ||
                                               x.Description.Contains(query.Keywords ?? string.Empty)))
                .Skip(query.PageIndex * query.PageSize).Take(query.PageSize)
                .Select(x => mapper.Map<CategoryModel>(x));
        }

        public IEnumerable<CategoryModel> Get(string? keywords)
        {
            return database.Categories.Where(x => x.IsDeleted != true &&
                                               (x.Name.Contains(keywords ?? string.Empty) ||
                                               x.Title.Contains(keywords ?? string.Empty) ||
                                               x.Description.Contains(keywords ?? string.Empty)))
                .Select(x => mapper.Map<CategoryModel>(x));
        }

        public Task<CategoryDetailModel> GetDetail(int Id)
        {
            var data = new CategoryDetailModel();

            var category = database.Categories.FirstOrDefault(x => x.Id == Id &&
                                                              x.IsDeleted != true);
            if (category != null)
            {
                mapper.Map(category, data);
                var postIds = database.PostCategories
                    .Where(x => x.IsDeleted != true &&
                                x.CategoryId == category.Id)
                    .Select(x => x.PostId)
                    .ToList();
                data.Blogs = database.Blogs
                    .Where(x => x.IsDeleted != true &&
                                postIds.Contains(x.Id))
                    .Select(x => mapper.Map<BlogModel>(x)).ToList();
            }

            return Task.FromResult(data);
        }
    }
}
