using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Logic.Configs;

namespace VIVU.Logic.CommandHandlers
{
    public class DeleteCategoryCommandHandler :
        IRequestHandler<DeleteCategoryCommand, CommonCommandResult>
    {
        private readonly AppDatabase database;
        private readonly ErrorConfig errorConfig;

        public DeleteCategoryCommandHandler(AppDatabase database,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResult> Handle(
            DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResult();

            try
            {
                var category = database.Categories.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

                if (category != null)
                {
                    category.MarkAsDeleted(request.UserName);
                    database.Categories.Update(category);
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
