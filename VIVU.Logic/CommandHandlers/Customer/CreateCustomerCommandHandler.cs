using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Logic.Configs;
using VIVU.Shared.Model;

namespace VIVU.Logic.CommandHandlers
{
    public class CreateCustomerCommandHandler 
        : IRequestHandler<CreateCustomerCommand, CommonCommandResultHasData<CustomerModel>>
    {
        private readonly AppDatabase database;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ErrorConfig errorConfig;

        public CreateCustomerCommandHandler(AppDatabase database,
            IMediator mediator,
            IMapper mapper,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.mediator = mediator;
            this.mapper = mapper;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResultHasData<CustomerModel>> Handle(
            CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResultHasData<CustomerModel>();

            try
            {
                var customerId = Nanoid.Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);
                request.Id = customerId;
                var customer = mapper.Map<Customer>(request);
                customer.SetCreatedAudit(request.UserName);

                database.Customers.Add(customer);
                database.SaveChanges();

                result.Success = true;
                result.Data = mapper.Map<CustomerModel>(customer);
            }
            catch (DbUpdateException ex)
            {
                if (database.Customers.SingleOrDefault(x => x.Id == request.Id) != null)
                {
                    result.Message = errorConfig.GetByKey("DuplicateCustomer");
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
