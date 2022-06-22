namespace VIVU.Logic.CommandHandlers;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, CommonCommandResultHasData<TagModel>>
{
    private readonly AppDatabase database;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public CreateTagCommandHandler(AppDatabase database,
        IMediator mediator,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mediator = mediator;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResultHasData<TagModel>> Handle(
        CreateTagCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<TagModel>();

        try
        {
            var tag = mapper.Map<Tag>(request);
            tag.SetCreatedAudit(request.UserName);

            database.Tags.Add(tag);
            database.SaveChanges();

            result.Success = true;
            result.Data = mapper.Map<TagModel>(tag);
        }
        catch (DbUpdateException ex)
        {
            if (database.Tags.SingleOrDefault(x => x.Id == request.Id) != null)
            {
                result.Message = errorConfig.GetByKey("DuplicateTag");
            }
            else
            {
                result.Message = ex.Message;
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}