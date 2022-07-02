namespace VIVU.Logic.Queries.Implements;

public class BlogQueries : IBlogQueries
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;

    public BlogQueries(AppDatabase database,
            IMapper mapper)
    {
        this.database = database;
        this.mapper = mapper;
    }
    public IEnumerable<BlogModel> Get(BlogQueryModel query)
    {
        return database.Blogs.Where(x => x.IsDeleted != true &&
                                           (x.Id.ToString().Contains(query.Keywords ?? string.Empty) ||
                                           x.Title.Contains(query.Keywords ?? string.Empty) ||
                                           x.Description.Contains(query.Keywords ?? string.Empty)
                                           )
                                         )
            .Select(x => mapper.Map<BlogModel>(x)).Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);
    }

    public IEnumerable<BlogModel> Get()
    {
        return database.Blogs.Where(x=>!x.IsDeleted)
            .Select(x => mapper.Map<BlogModel>(x));
    }

    public Task<BlogDetailModel> GetDetail(int Id)
    {
        var data = new BlogDetailModel();

        var blog = database.Blogs.FirstOrDefault(x => x.Id == Id &&
                                                          x.IsDeleted != true);
        if (blog != null)
        {
            mapper.Map(blog, data);
            var listCategory = database.PostCategories.Where(x => !x.IsDeleted && x.PostId == Id)
                  .Select(x => x.CategoryId).ToList();
            var category = database.Categories.Where(x=>listCategory.Contains(x.Id) && !x.IsDeleted)
                .Select(x => mapper.Map<CategoryModel>(x)).ToList();

            var listTag = database.PostTags.Where(x => !x.IsDeleted && x.BlogId == Id)
                .Select(x => x.TagId).ToList();
            var tag =  database.Tags.Where(x => !x.IsDeleted && listTag.Contains(x.Id))
                .Select(x => mapper.Map<TagModel>(x)).ToList();

            data.Tags = tag;
            data.Categories = category;
        }

        return Task.FromResult(data);
    }
}

