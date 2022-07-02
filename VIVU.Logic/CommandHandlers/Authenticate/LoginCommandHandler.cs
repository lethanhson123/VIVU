
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using VIVU.Ultils.Helpers;

namespace VIVU.Logic.CommandHandlers;
public class LoginCommandHandler
    : IRequestHandler<LoginCommand, CommonCommandResultHasData<AuthenticateModel>>
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly AppDatabase appDatabase;
    private readonly ErrorConfig errorConfig;
    private readonly AuthenticateConfig authenConfig;
    public LoginCommandHandler(
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

    public async Task<CommonCommandResultHasData<AuthenticateModel>> Handle(LoginCommand request,
            CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<AuthenticateModel>();

        try
        {
            var user = await userManager.FindByNameAsync(request.UserName).ConfigureAwait(false);

            if (user == null)
            {
                result.SetResult(false, errorConfig.GetByKey("InvalidAuthenticate"));
                return result;
            }
            var login = await signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: true).ConfigureAwait(false);

            if (login.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);

                var token = GenerateToken(user, userRoles);
                var refreshToken = GenerateRefreshToken(token);
                await appDatabase.UserTokens.AddAsync(new UserToken()
                {
                    UserName = request.UserName ?? String.Empty,
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    IssuedAt = DateTime.Now,
                    IsExpired = false
                });
                appDatabase.SaveChanges();
                var dataResponse = new AuthenticateModel
                {
                    RefreshToken = refreshToken,
                    AccessToken = token,
                    ExpiredDate = DateTime.Now.AddMinutes(authenConfig.TokenExpireAfterMinutes)
                };
                result.SetData(dataResponse).SetResult(true, "");
                return result;
            }
            else
            {
                result.SetResult(false, errorConfig.GetByKey("LoginFail"));
                return result;
            }
        }
        catch (Exception ex)
        {

            result.SetResult(false, ex.Message.ToString());
            return result;
        }

    }

    private string GenerateToken(User user, IList<string>? roles = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(authenConfig.SecretKey ?? String.Empty);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id) ,
                    new Claim("roles", roles?.Count > 0 ? string.Join("$", roles) : string.Empty)
                }),
            Expires = DateTime.Now.AddMinutes(authenConfig.TokenExpireAfterMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static string GenerateRefreshToken(string token) => SecurityHelper.CreateMD5(token);
}

