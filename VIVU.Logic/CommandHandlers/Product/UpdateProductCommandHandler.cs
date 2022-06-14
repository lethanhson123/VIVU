namespace VIVU.Logic.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public UpdateProductCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();
        try
        {
            var product = database.Products.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

            if (product != null)
            {
                mapper.Map(request, product);
                product.SetUpdatedAudit(request.UserName);

                database.Products.Update(product);
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