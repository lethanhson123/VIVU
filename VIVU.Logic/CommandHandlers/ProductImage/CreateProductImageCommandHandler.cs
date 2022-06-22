namespace VIVU.Logic.CommandHandlers;

public class CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, CommonCommandResultHasData<ProductImageModel>>
{
    private readonly AppDatabase database;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public CreateProductImageCommandHandler(AppDatabase database,
        IMediator mediator,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mediator = mediator;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResultHasData<ProductImageModel>> Handle(
        CreateProductImageCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<ProductImageModel>();

        try
        {
            
            var product = mapper.Map<ProductImage>(request);
            product.SetCreatedAudit(request.UserName);

            database.ProductImages.Add(product);
            database.SaveChanges();

            result.Success = true;
            result.Data = mapper.Map<ProductImageModel>(product);
        }
        catch (DbUpdateException ex)
        {
            if (database.ProductImages.SingleOrDefault(x => x.Id == request.Id) != null)
            {
                result.Message = errorConfig.GetByKey("DuplicateProduct");
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
