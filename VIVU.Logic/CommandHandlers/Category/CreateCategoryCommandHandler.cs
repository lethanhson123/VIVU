using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Logic.Configs;
using VIVU.Shared.Model;

namespace VIVU.Logic.CommandHandlers
{
    public class CreateCategoryCommandHandler
        : IRequestHandler<CreateCategoryCommand, CommonCommandResultHasData<CategoryModel>>
    {
        private readonly AppDatabase database;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ErrorConfig errorConfig;

        public CreateCategoryCommandHandler(AppDatabase database,
            IMediator mediator,
            IMapper mapper,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.mediator = mediator;
            this.mapper = mapper;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResultHasData<CategoryModel>> Handle(
            CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResultHasData<CategoryModel>();

            try
            {
                var category = mapper.Map<Category>(request);
                category.SetCreatedAudit(request.UserName);

                database.Categories.Add(category);
                database.SaveChanges();

                result.Success = true;
                result.Data = mapper.Map<CategoryModel>(category);
            }
            catch (DbUpdateException ex)
            {
                if (database.Categories.SingleOrDefault(x => x.Id == request.Id) != null)
                {
                    result.Message = errorConfig.GetByKey("DuplicateCategory");
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
}
