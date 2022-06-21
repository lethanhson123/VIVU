
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using VIVU.Ultils.Helpers;

namespace VIVU.Logic.CommandHandlers;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, CommonCommandResultHasData<AuthenticateModel>>
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly AuthenticateConfig authenticateConfig;
    private readonly ErrorConfig errorConfig;
    private readonly AppDatabase appDatabase;

    public RefreshTokenCommandHandler(
        UserManager<User> userManager, 
        SignInManager<User> signInManager, 
        AppDatabase appDatabase, 
        IOptions<AuthenticateConfig> authenticateConfig,
        IOptions<ErrorConfig> errorConfig)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.appDatabase = appDatabase;
        this.authenticateConfig = authenticateConfig.Value;
        this.errorConfig = errorConfig.Value;
    }
    public async Task<CommonCommandResultHasData<AuthenticateModel>> Handle(RefreshTokenCommand request,
            CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<AuthenticateModel>();

        try
        {
            var userToken = await appDatabase.UserTokens.FirstOrDefaultAsync(x => x.IsExpired == false && x.RefreshToken == request.RefreshToken);
            if (userToken != null)
            {
                var user = await appDatabase.Users.FirstOrDefaultAsync(x => x.UserName == userToken.UserName);
                var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                var token = GenerateToken(user, userRoles);

              

                var oldRefreshTokens = appDatabase.UserTokens.Where(x => x.RefreshToken == request.RefreshToken &&
                                       x.IsExpired == false);

                await oldRefreshTokens.ForEachAsync(x =>
                {
                    x.IsExpired = true;
                });
                var dataResponse = new AuthenticateModel
                {
                    RefreshToken = userToken.RefreshToken,
                    TokenExpireTime = authenticateConfig.TokenExpireAfterMinutes,
                    AccessToken = token
                };

                /// Add new token                var refreshToken = GenerateRefreshToken(token);
                await appDatabase.UserTokens.AddAsync(new UserToken()
                {
                    UserName = userToken.UserName ?? String.Empty,
                    AccessToken = token,
                    RefreshToken = userToken.RefreshToken,
                    IssuedAt = DateTime.Now,
                    IsExpired = false
                });
                appDatabase.UserTokens.UpdateRange(oldRefreshTokens);

                await appDatabase.SaveChangesAsync();


              

                result.SetData(dataResponse).SetResult(true, "");
                return result;
            }
            else
            {
                result.SetResult(false, "Lấy token thất bại.");

                return result;
            }
        }
        catch (Exception ex)
        {

            result.SetResult(false, ex.Message.ToString());
            return result;
        }

    }
    private string GenerateToken(User user, IList<string> roles = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(authenticateConfig.SecretKey ?? String.Empty);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id) ,
                    new Claim("roles", roles?.Count > 0 ? string.Join("$", roles) : string.Empty)
                }),
            Expires = DateTime.Now.AddMinutes(authenticateConfig.TokenExpireAfterMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static string GenerateRefreshToken(string token) => SecurityHelper.CreateMD5(token);
}

