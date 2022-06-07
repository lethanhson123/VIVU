namespace VIVU.Logic.CommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CommonCommandResultHasData<ProductModel>>
{
    private readonly AppDatabase database;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public CreateProductCommandHandler(AppDatabase database,
        IMediator mediator,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mediator = mediator;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResultHasData<ProductModel>> Handle(
        CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<ProductModel>();

        try
        {
            var productId = Nanoid.Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);
            request.Id = productId;
            var product = mapper.Map<Product>(request);
            product.SetCreatedAudit(request.UserName);

            database.Products.Add(product);
            database.SaveChanges();

            result.Success = true;
            result.Data = mapper.Map<ProductModel>(product);
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
