namespace VIVU.Logic.CommandHandlers;

public class DeleteTagCommandHandler :
    IRequestHandler<DeleteTagCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public DeleteTagCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var tag = database.Tags.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

            if (tag != null)
            {
                mapper.Map(request, tag);
                tag.MarkAsDeleted(request.UserName);

                database.Update(tag);
                database.SaveChanges();
                result.Success = true;
            }
            else
            {
                result.Message = errorConfig.GetByKey("NotFoundTag");
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}