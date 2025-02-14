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
        //this has been added for adding to basket
        BindingContext = new ProductsViewModel();
        
    }
    public ProductsViewModel ProductsViewModel
    {
        set
        {
            BindingContext = value;
            Debug.WriteLine($"ViewModel is set: {BindingContext != null}");
        }
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Debug.WriteLine($"BindingContext is: {BindingContext?.GetType().Name}");

    }

}