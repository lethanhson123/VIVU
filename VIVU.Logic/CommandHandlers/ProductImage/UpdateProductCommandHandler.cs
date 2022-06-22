namespace VIVU.Logic.CommandHandlers;

public class UpdateProductImageCommandHandler : IRequestHandler<UpdateProductImageCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public UpdateProductImageCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var product = database.ProductImages.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

            if (product != null)
            {
                mapper.Map(request, product);
                product.SetUpdatedAudit(request.UserName);

                database.ProductImages.Update(product);
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