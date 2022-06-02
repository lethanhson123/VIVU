using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Logic.Configs;

namespace VIVU.Logic.CommandHandlers
{
    public class DeleteCustomerCommandHandler :
        IRequestHandler<DeleteCustomerCommand, CommonCommandResult>
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;
        private readonly ErrorConfig errorConfig;

        public DeleteCustomerCommandHandler(AppDatabase database,
            IMapper mapper,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.mapper = mapper;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResult();

            try
            {
                var customer = database.Customers.FirstOrDefault(x => x.Id == request.Id);

                if (customer != null)
                {
                    mapper.Map(request, customer);
                    customer.SetUpdatedAudit(request.UserName);

                    database.Update(customer);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Message = errorConfig.GetByKey("NotFoundCustomer");
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
