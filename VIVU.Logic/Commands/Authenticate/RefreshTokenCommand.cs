namespace VIVU.Logic.Commands;
public class RefreshTokenCommand : IRequest<CommonCommandResultHasData<AuthenticateModel>>
{
    public string? RefreshToken { get; set; }
}
