namespace VIVU.Logic.CommandHandlers;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CommonCommandResultHasData<BlogModel>>
{
    private readonly AppDatabase applicationDatabase;
    private readonly IMapper mapper;

    public CreateBlogCommandHandler(AppDatabase applicationDatabase, IMapper mapper)
    {
        this.applicationDatabase = applicationDatabase;
        this.mapper = mapper;
    }
    public Task<CommonCommandResultHasData<BlogModel>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
      
        var result = new CommonCommandResultHasData<BlogModel>();

        try
        {
            var blog = mapper.Map<VIVU.Data.Entities.Blog>(request);
            blog.SetCreatedAudit(request.UserName);
            applicationDatabase.Blogs.Add(blog);

            var tag = mapper.Map<List<BlogTag>>(request.Tags);
            tag.ForEach(x => x.BlogId = blog.Id);
            applicationDatabase.PostTags.AddRange(tag);

            var category = mapper.Map<List<BlogCategory>>(request.Categories);
            category.ForEach(x => x.PostId = blog.Id);

            applicationDatabase.PostCategories.AddRange(category);
            applicationDatabase.SaveChanges();
            result.Data = mapper.Map<BlogModel>(blog);
            result.Success = true;
        }
        catch (DbUpdateException ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}
