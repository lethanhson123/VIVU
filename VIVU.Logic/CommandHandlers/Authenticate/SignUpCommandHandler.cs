
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using VIVU.Ultils.Helpers;

namespace VIVU.Logic.CommandHandlers;
public class SignUpCommandHandler
    : IRequestHandler<SignUpCommand, CommonCommandResult>
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly AppDatabase appDatabase;
    private readonly ErrorConfig errorConfig;
    private readonly AuthenticateConfig authenConfig;
    public SignUpCommandHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            AppDatabase appDatabase,
            IOptions<AuthenticateConfig> authenConfig,
            IOptions<ErrorConfig> errorConfig

        )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.appDatabase = appDatabase;
        this.errorConfig = errorConfig.Value;
        this.authenConfig = authenConfig.Value;
    }

    public async Task<CommonCommandResult> Handle(SignUpCommand request,
            CancellationToken cancellationToken)
    {
        try
        {
            if (request.VerifySignUp != authenConfig.VerifySignUp)
            {
                return new CommonCommandResult()
                {
                    Success = false,
                    Message = "Chuỗi xác thực không đúng!"
                };
            }
            var userExisted = await userManager.FindByNameAsync(request.UserName).ConfigureAwait(false);
           
            if (userExisted != null)
            {
                return new CommonCommandResult()
                {
                    Success = false,
                    Message = "Tên tài khoản đang tồn tại."
                };
            }

            User newUser = new User()
            {
                UserName = request.UserName
            };

            var result = await userManager.CreateAsync(newUser, request.Password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                //TODO
                //var user = await _userManager.FindByNameAsync(newUser.UserName).ConfigureAwait(false);
                //var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                return new CommonCommandResult
                {
                    Success = true
                };
            }
            return new CommonCommandResult()
            {
                Success = false,
                Message = string.Join(",", result.Errors.Select(x => x.Description))
            };
        }
        catch (Exception ex)
        {
            return new CommonCommandResult()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }


}

