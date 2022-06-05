using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Logic.Configs;

namespace VIVU.Logic.CommandHandlers
{
    public class UpdateCategoryCommandHandler :
        IRequestHandler<UpdateCategoryCommand, CommonCommandResult>
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;
        private readonly ErrorConfig errorConfig;

        public UpdateCategoryCommandHandler(AppDatabase database,
            IMapper mapper,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.mapper = mapper;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResult();

            try
            {
                var category = database.Categories.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

                if (category != null)
                {
                    mapper.Map(request, category);
                    category.SetUpdatedAudit(request.UserName);

                    database.Update(category);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Message = errorConfig.GetByKey("NotFoundCategory");
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
