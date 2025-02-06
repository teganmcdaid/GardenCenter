using GardenCenter.Model;
using GardenCenter.ViewModels;
using System.Diagnostics;

namespace GardenCenter.Views;

//need to use query attribute to pass the property to products view model
[QueryProperty(nameof(ProductsViewModel), "ProductsViewModel")]
public partial class ProductsPage : ContentPage
{
    //need parameterless constructor for nevigating
    public ProductsPage()
    {
        InitializeComponent();
    }
    public ProductsViewModel ProductsViewModel
    {
        set
        {
            BindingContext = value;
            // check if products are passed correctly
            //Debug.WriteLine($"Products count: {value.Products.Count}");
        }
    }
    //public ProductsPage(ProductsViewModel productsViewModel)
    //{
    //    InitializeComponent();
    //    BindingContext = productsViewModel;
    //    // Check if products are passed correctly
    //    Debug.WriteLine($"Products count: {productsViewModel.Products.Count}");
    //}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}