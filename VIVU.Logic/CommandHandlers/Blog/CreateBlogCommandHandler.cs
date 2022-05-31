namespace VIVU.Logic.CommandHandlers;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CommonCommandResultHasData<VIVU.Data.Entities.Blog>>
{
    private readonly AppDatabase applicationDatabase;
    private readonly IMapper mapper;

    public CreateBlogCommandHandler(AppDatabase applicationDatabase, IMapper mapper)
    {
        this.applicationDatabase = applicationDatabase;
        this.mapper = mapper;
    }
    public Task<CommonCommandResultHasData<VIVU.Data.Entities.Blog>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var model = mapper.Map<VIVU.Data.Entities.Blog>(request);
        var result = new CommonCommandResultHasData<VIVU.Data.Entities.Blog>();

        try
        {
            model.SetCreateAudit(request.UserName);
            applicationDatabase.Blogs.Add(model);
            applicationDatabase.SaveChanges();

            result.SetData(model);
            result.Success = true;
        }
        catch (DbUpdateException ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}
