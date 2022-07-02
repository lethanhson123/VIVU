namespace VIVU.Logic.CommandHandlers;
public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, CommonCommandResult>
{
    private readonly AppDatabase applicationDatabase;
    private readonly IMapper mapper;

    public UpdateBlogCommandHandler(AppDatabase applicationDatabase, IMapper mapper)
    {
        this.applicationDatabase = applicationDatabase;
        this.mapper = mapper;
    }
    public Task<CommonCommandResult> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
     
            var blog = applicationDatabase.Blogs.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

            if (blog != null )
            {
                blog.SetUpdatedAudit(request.UserName);
                blog.Content = request.Content;
                blog.PostDate = request.PostDate;
                blog.Description = request.Description;
                blog.Image = request.Image;
                blog.Summary = request.Summary;
                blog.Keywords = request.Keywords;
                blog.Title = request.Title;
                blog.Meta = request.Meta;
                applicationDatabase.Blogs.Update(blog);

                var tagOld = applicationDatabase.PostTags.Where(x => x.BlogId == request.Id).ToList();
                tagOld.ForEach(x => x.MarkAsDeleted(request.UserName));
                applicationDatabase.PostTags.UpdateRange(tagOld);

                var tag = mapper.Map<List<BlogTag>>(request.Tags);
                tag.ForEach(x => x.BlogId = blog.Id);

                applicationDatabase.PostTags.AddRange(tag);

                var categoryOld = applicationDatabase.PostCategories.Where(x => x.PostId == request.Id).ToList();
                categoryOld.ForEach(x => x.MarkAsDeleted(request.UserName));
                applicationDatabase.PostCategories.UpdateRange(categoryOld);

                var category = mapper.Map<List<BlogCategory>>(request.Categories);
                category.ForEach(x => x.PostId = blog.Id);

                applicationDatabase.PostCategories.AddRange(category);
                applicationDatabase.SaveChanges();
                result.Success = true;

            }
            else
            {
                result.Message = "Not found";
            }
        }
        catch (DbUpdateException ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}


