using Microsoft.JSInterop;

namespace KalastusWebsite.Services
{
    public class UserSession
    {
        private readonly IJSRuntime _jsRuntime;
        public bool IsLoaded { get; private set; } = false;

        public UserSession(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public string Username { get; set; }
        public int UserId { get; set; }
        public bool IsLoggedIn { get; set; } = false;
        public string? RedirectUrl { get; set; }

        public async Task PersistSession()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "username", Username);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userId", UserId.ToString());
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", IsLoggedIn.ToString());
        }

        public async Task LoadSession()
        {
            Username = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "username") ?? "";
            UserId = int.Parse(await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userId") ?? "0");
            IsLoggedIn = bool.Parse(await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "isLoggedIn") ?? "false");
            IsLoaded = true;
        }

        public async Task ClearSession()
        {
            Username = "";
            UserId = 0;
            IsLoggedIn = false;
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}