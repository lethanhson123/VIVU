namespace VIVU.Logic.CommandHandlers;
public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, CommonCommandResultHasData<VIVU.Data.Entities.Blog>>
{
    private readonly AppDatabase applicationDatabase;
    private readonly IMapper mapper;

    public UpdateBlogCommandHandler(AppDatabase applicationDatabase, IMapper mapper)
    {
        this.applicationDatabase = applicationDatabase;
        this.mapper = mapper;
    }
    public Task<CommonCommandResultHasData<VIVU.Data.Entities.Blog>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var model = applicationDatabase.Blogs.FirstOrDefault(x => x.Id == request.Id);
        var result = new CommonCommandResultHasData<VIVU.Data.Entities.Blog>();

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


