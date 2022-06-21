namespace VIVU.Shared.Model;

public class AuthenticateModel
{
    public string UserName { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public int TokenExpireTime { get; set; }
}
