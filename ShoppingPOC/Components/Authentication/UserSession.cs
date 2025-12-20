using Models;

namespace ShoppingPOC.Components.Authentication
{
    public class UserSession
    {
        public User? CurrentUser { get; set; }

        // You can add a helper property for clarity
        public bool IsLoggedIn => CurrentUser != null;
    }
}
