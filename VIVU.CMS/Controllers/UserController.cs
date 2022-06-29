namespace VIVU.CMS.Controllers;
public class UserController : Controller
{
    private readonly IUserService userService;
    private readonly ErrorConfig errorConfig;
    private readonly AuthenticateConfig authenConfig;

    public UserController(IUserService userService,
        IOptions<ErrorConfig> errorConfig,
        IOptions<AuthenticateConfig> authenConfig)
    {
        this.userService = userService;
        this.authenConfig = authenConfig.Value;
        this.errorConfig = errorConfig.Value;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult InfoModal()
    {
        return PartialView();
    }

    public IActionResult Login(LoginViewModel model)
    {
        return View(model);
    }

    public async Task<ActionResult> LoginSubmit(LoginViewModel model)
    {
        var response = new CommonResponseModel<LoginViewModel>();

        if (ModelState.IsValid)
        {
            var remoteResponse = userService.Login(new LoginRequest()
            {
                UserName = model.UserName!,
                Password = model.Password!,
            });

            if (remoteResponse.Result.Success)
            {
                var remoteData = remoteResponse.Result.Data;

                if (remoteData != null)
                {
                    response.Success = true;

                    var claims = new List<Claim>()
                    {
                        new Claim("UserName", remoteData.UserName),
                        new Claim("AccessToken", remoteData.AccessToken),
                        new Claim("RefreshToken", remoteData.RefreshToken),
                        new Claim("IsAuthenticate", "true"),
                        new Claim("IsRememberPassword", model.RememberPassword.ToString()),
                        new Claim("ExpiredDate", remoteData.ExpiredDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? DateTime.MaxValue.ToString()),
                    };

                    var claimIdentites = new List<ClaimsIdentity>()
                    {
                        new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme),
                    };

                    var claimPrincipal = new ClaimsPrincipal(claimIdentites);
                    HttpContext.Response.Cookies.Append("UserName", remoteData.UserName);
                    HttpContext.Response.Cookies.Append("AuthorId", remoteData.AuthorId);

                    await HttpContext.SignInAsync(claimPrincipal);

                    model.RedirectUrl = string.IsNullOrEmpty(model.ReturnUrl) ?
                        authenConfig.DefaultRedirect : model.ReturnUrl;

                    response.Success = true;
                    response.Data = model;
                }
                else
                {
                    response.Message = errorConfig.GetByKey("Unknown");
                }
            }
            else
            {
                response.Message = errorConfig.GetByKey("Unknown");
            }
        }
        else
        {
            var error = string.Join(";", ModelState.GetError());
            response.Message = error;
        }

        return Json(response);
    }
}
