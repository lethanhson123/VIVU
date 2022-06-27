namespace VIVU.Logic.CommandHandlers;

public class DeleteProductCategoryCommandHandler :
        IRequestHandler<DeleteProductCategoryCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public DeleteProductCategoryCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var productCategory = database.ProductCategories.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

            if (productCategory != null)
            {
                mapper.Map(request, productCategory);
                productCategory.MarkAsDeleted(request.UserName);

                database.ProductCategories.Update(productCategory);
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

