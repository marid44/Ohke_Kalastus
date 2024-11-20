namespace KalastusWebsite.Services
{
    public class UserSession
    {
        public int UserId { get; set; } // Käyttäjän ID
        public string Username { get; set; } // Käyttäjän nimi
        public bool IsLoggedIn { get; set; } = false; // Onko käyttäjä kirjautunut sisään
        public string? RedirectUrl { get; set; } // Uudelleenohjaus URL kirjautumisen jälkeen
    }
}
