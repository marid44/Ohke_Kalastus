namespace KalastusWebsite.Services
{
    public class UserSession
    {
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; } = false;
        public string? RedirectUrl { get; set; }  // Stores the intended redirect URL after login
    }
}