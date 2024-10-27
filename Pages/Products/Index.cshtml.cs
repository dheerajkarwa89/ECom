using ECom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ECom.Models;


namespace ECom.Pages.Products
{
    public class ProductsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ProductsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Product> Items { get; set; } = new List<Product>();

        //define the OnGetAsync method to fetch products from the Web API and store them in a list.
        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7166/api/product");
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var jsonString = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<Product>>(jsonString, options);
            }
        }
    }
}