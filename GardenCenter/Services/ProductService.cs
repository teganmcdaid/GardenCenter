using GardenCenter.Model;
using System.Diagnostics;
using System.Net.Http.Json;

namespace GardenCenter.Services;

public class ProductService
{
	
    
    HttpClient httpClient;

    public ProductService()
    {
        httpClient = new HttpClient();
    }

    List<Products> productList = new();

    //want to read json file and store products in a list
    public async Task<List<Products>> GetProducts()
    {
        if (productList?.Count > 0)
            return productList;

        var url = "https://raw.githubusercontent.com/teganmcdaid/GardenCenter/master/productdetails.json";

        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            productList = await response.Content.ReadFromJsonAsync<List<Products>>();
        }
        else
        {
            productList = new List<Products>();
        }

        //check number of products being fetched
        Debug.WriteLine($"ProductService fetched {productList.Count} products.");

        return productList;
    }
}
