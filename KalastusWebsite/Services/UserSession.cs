namespace KalastusWebsite.Services
{
    public class UserSession
    {
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; } = false;
    }
}