

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
        var category = applicationDatabase.Blogs.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);
        var result = new CommonCommandResult();

        try
        {
            if (category != null)
            {
                category.MarkAsDeleted(request.UserName);
                applicationDatabase.Update(category);            
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

