﻿namespace VIVU.Logic.CommandHandlers;
public class ChangePasswordCommandHandler
    : IRequestHandler<ChangePasswordCommand, CommonCommandResult>
{
    private readonly AppDatabase context;
    private readonly SignInManager<User> signInManager;
    private readonly IMapper mapper;

    public ChangePasswordCommandHandler(AppDatabase context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper)
    {
        this.context = context;
        this.signInManager = signInManager;
        this.mapper = mapper;
    }

    public Task<CommonCommandResult> Handle(ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {

        }
        catch (DbUpdateException ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}
