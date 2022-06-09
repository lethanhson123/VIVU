namespace VIVU.Logic.Commands;
public class LoginCommand : IRequest<CommonCommandResultHasData<AuthenticateModel>>
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
