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
        var model = applicationDatabase.Blogs.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);
        var result = new CommonCommandResult();

        try
        {
            if (model != null)
            {
                mapper.Map(request, model);
                applicationDatabase.Blogs.Update(model);
                model.SetUpdatedAudit(request.UserName);
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


