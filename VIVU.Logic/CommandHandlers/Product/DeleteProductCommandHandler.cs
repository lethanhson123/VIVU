namespace VIVU.Logic.CommandHandlers;

public class DeleteProductCommandHandler :
        IRequestHandler<DeleteProductCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public DeleteProductCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var product = database.Products.FirstOrDefault(x => x.Id == request.Id);

            if (product != null)
            {
                mapper.Map(request, product);
                product.SetUpdatedAudit(request.UserName);

                database.Update(product);
                database.SaveChanges();
                result.Success = true;
            }
            else
            {
                result.Message = errorConfig.GetByKey("NotFoundProduct");
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}

