using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ShoppingPOC.Components.Pages
{
    public partial class Home
    {

        [Inject]
        private NavigationManager _navManager { get; set; }

        private List<Product> products;
        protected override async Task OnInitializedAsync()
        {
            // 1. Check Login Status first
            if (!Session.IsLoggedIn)
            {
                // Redirect and set 'forceLoad' if necessary (usually false is fine for internal routing)
                _navManager.NavigateTo("/Login");

                // CRITICAL: Return immediately so code below does not run
                return;
            }

            // 2. Only load data if the user is actually logged in
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            // Usar la fábrica para crear un DbContext y asegurar que se libera
            await using var context = await DbContextFactory.CreateDbContextAsync();
            products = await context.Product.ToListAsync() ?? new();
        }
    }
}
