namespace VIVU.Logic.CommandHandlers;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public UpdateTagCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var tag = database.Tags.FirstOrDefault(x => x.Id == request.Id);

            if (tag != null)
            {
                mapper.Map(request, tag);
                tag.SetUpdatedAudit(request.UserName);

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