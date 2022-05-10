

namespace VIVU.Logic.CommandHandlers.Blog;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, CommonCommandResult>
{
    private readonly ApplicationDbContext applicationDatabase;
    private readonly IMapper mapper;
    public DeleteBlogCommandHandler(ApplicationDbContext applicationDatabase,
        IMapper mapper)
    {
        this.applicationDatabase = applicationDatabase;
        this.mapper = mapper;
    }
    public Task<CommonCommandResult> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var category = applicationDatabase.Blogs.FirstOrDefault(x => x.Id == request.Id);
        var result = new CommonCommandResult();

        try
        {
            if (category != null)
            {
                applicationDatabase.Blogs.Remove(category);
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

