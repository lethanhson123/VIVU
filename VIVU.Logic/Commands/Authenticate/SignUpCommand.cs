namespace VIVU.Logic.Commands;

public class SignUpCommand : IRequest<CommonCommandResult>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string VerifySignUp { get; set; } = string.Empty;
}
