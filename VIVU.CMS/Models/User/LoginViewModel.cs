namespace VIVU.CMS.Models
{
    public class LoginViewModel
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public bool RememberPassword { get; set; }
        public string? ReturnUrl { get; set; } = string.Empty;
        public string? RedirectUrl { get; set; } = string.Empty;
        public string? ErrorUrl { get; set; } = string.Empty;
    }
}
