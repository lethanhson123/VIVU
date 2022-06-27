namespace VIVU.Logic.CommandHandlers;

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, CommonCommandResultHasData<ProductCategoryModel>>
{
    private readonly AppDatabase database;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public CreateProductCategoryCommandHandler(AppDatabase database,
        IMediator mediator,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mediator = mediator;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResultHasData<ProductCategoryModel>> Handle(
        CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<ProductCategoryModel>();

        try
        {
            var productId = Nanoid.Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);
            request.Id = productId;
            var product = mapper.Map<ProductCategory>(request);
            product.SetCreatedAudit(request.UserName);

            database.ProductCategories.Add(product);
            database.SaveChanges();

            result.Success = true;
            result.Data = mapper.Map<ProductCategoryModel>(product);
        }
        catch (DbUpdateException ex)
        {
            if (database.Products.SingleOrDefault(x => x.Id == request.Id) != null)
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
