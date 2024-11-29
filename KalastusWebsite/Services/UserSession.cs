namespace KalastusWebsite.Services
{
    public class UserSession
    {
        public string Username { get; set; }

        public int UserId { get; set; } // Add UserId to track the logged-in user
        public bool IsLoggedIn { get; set; } = false;
        public string? RedirectUrl { get; set; }  // Stores the intended redirect URL after login
    }
}