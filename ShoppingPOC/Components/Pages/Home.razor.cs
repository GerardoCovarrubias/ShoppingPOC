using Microsoft.EntityFrameworkCore;
using Models;

namespace ShoppingPOC.Components.Pages
{
    public partial class Home
    {
        
    private List<Products>? products;

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            // Usar la fábrica para crear un DbContext y asegurar que se libera
            await using var context = await DbContextFactory.CreateDbContextAsync();
            products = await context.Products.ToListAsync() ?? new();
        }
    }
}
