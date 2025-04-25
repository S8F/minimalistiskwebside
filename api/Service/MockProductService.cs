using minimalistiskdotcom.api.Models;

namespace minimalistiskdotcom.api.Service;
public class MockProductService
{
    public List<Product> GetProducts()
    {
        return new List<Product>
            {
                new Product { Name = "Produkt A", Description = "Kort beskrivelse", Price = 149.99m, ImageUrl = "https://via.placeholder.com/400" },
                new Product { Name = "Produkt B", Description = "En anden beskrivelse", Price = 199.95m, ImageUrl = "https://via.placeholder.com/400" },
                new Product { Name = "Produkt C", Description = "Beskrivelse her", Price = 89.00m, ImageUrl = "https://via.placeholder.com/400" }
            };
    }
}
