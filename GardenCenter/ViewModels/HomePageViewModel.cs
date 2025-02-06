using CommunityToolkit.Mvvm.Input;
using GardenCenter.Model;
using GardenCenter.Services;
using GardenCenter.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace GardenCenter.ViewModels;

public partial class HomePageViewModel: BaseViewModel
{
    ProductService productService;

    public ObservableCollection<Products> Products { get; set; } = new();

    public HomePageViewModel()
    {
        Products = new ObservableCollection<Products>();
    }
    public HomePageViewModel(ProductService productService)
    {
        this.productService = productService;
    }

    //navigating to the prodcut page
    [RelayCommand]
    async Task GoToProducts(List<Products> products)
    {
        //if there is no products return
        if (products is null)
        {
            //notify user there are no products
            await Shell.Current.DisplayAlert("No Products", "No products are available.", "OK");
            return;
        }

        var viewModel = new ProductsViewModel(products);
        //check number of products being passed through
        Debug.WriteLine($"Navigating to ProductsPage with {products.Count} products.");

        //wont navigate with shell alone add nav parameter to see if works
        var navigationParameter = new Dictionary<string, object>
    {
        { "ProductsViewModel", viewModel }
    };

        await Shell.Current.GoToAsync(nameof(ProductsPage), navigationParameter);
    
    //go to products page and pass through products
    //await Shell.Current.GoToAsync($"//{nameof(ProductsPage)}", true,
    //        new Dictionary<string, object>
    //        {
    //            {"ProductsViewModel", viewModel}
    //        });
    }


    //call get products by category and pass "Tools"
    [RelayCommand]
     async Task GetToolsAsync()
    {
        await GetProductsByCategoryAsync("Tools");
    }

    //call get products by category and pass "Plants"
    [RelayCommand]
    async Task GetPlantsAsync()
    {
        await GetProductsByCategoryAsync("Plants");
    }

    //call get products by category and pass "Garden Care"
    [RelayCommand]
    async Task GetGardenCareAsync()
    {
        await GetProductsByCategoryAsync("Garden Care");
    }

    private async Task GetProductsByCategoryAsync(string category)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var products = await productService.GetProducts();

            //check how many products are passing through before filtering
            Debug.WriteLine($"Fetched {products.Count} products from the service.");

            //if products is not empty clear it before adding more
            if (Products.Count != 0)
                Products.Clear();

            //if product matches category add to products
            foreach (var product in products)
            {
                if (product.Category == category)
                {
                    Products.Add(product);
                }
            }

            //check how many products after filtering
            Debug.WriteLine($"Filtered {Products.Count} products for category '{category}'.");


            //go to products
            await GoToProducts(Products.ToList());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get Products : {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

}


