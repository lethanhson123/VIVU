

namespace VIVU.Logic.CommandHandlers.Blog;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, CommonCommandResult>
{
    private readonly AppDatabase applicationDatabase;
    private readonly IMapper mapper;
    public DeleteBlogCommandHandler(AppDatabase applicationDatabase,
        IMapper mapper)
    {
        this.applicationDatabase = applicationDatabase;
        this.mapper = mapper;
    }
    public Task<CommonCommandResult> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var blog = applicationDatabase.Blogs.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

            if (blog != null)
            {
                blog.MarkAsDeleted(request.UserName);
                applicationDatabase.Blogs.Update(blog);

                var tag = applicationDatabase.PostTags.Where(x => x.BlogId == request.Id).ToList();
                tag.ForEach(x => x.MarkAsDeleted(request.UserName));
                applicationDatabase.PostTags.UpdateRange(tag);



                var category = applicationDatabase.PostCategories.Where(x => x.PostId == request.Id).ToList();
                category.ForEach(x => x.MarkAsDeleted(request.UserName));
                applicationDatabase.PostCategories.UpdateRange(category);

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

