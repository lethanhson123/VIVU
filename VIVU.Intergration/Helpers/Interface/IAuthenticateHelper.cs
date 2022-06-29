
namespace VIVU.Intergration.Interface;

public interface IAuthenticateHelper
{
    string RefreshToken { get; set; }
    string AccessToken { get; set; }
    DateTime? ExpiredDate { get; set; }
    HttpContext? context { get; set; }

    public void SetInforFromContext(HttpContext context);
    public void SetResponse(HttpContext context);
    public string GetAccessToken();
}
