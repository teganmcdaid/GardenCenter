
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GardenCenter.Model;
using GardenCenter.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace GardenCenter.ViewModels;
public partial class ProductsViewModel : BaseViewModel
{

    //Products list to store products being passed through
    public ObservableCollection<Products> Products { get; set; } = new();
    public BasketViewModel BasketViewModel { get; set; }
    public string currentUserNumber = Preferences.Get("PhoneNumber", "Unknown");
    //check for quanity changing to notify the Ui so label will change
    public event PropertyChangedEventHandler PropertyChanged;



    public ProductsViewModel()
    {
        BasketViewModel = new BasketViewModel(currentUserNumber);
        

    }

    public ProductsViewModel(List<Products> products)
    {
        Products = new ObservableCollection<Products>(products);
        BasketViewModel = new BasketViewModel(currentUserNumber);
        
    }

    [RelayCommand]
    async Task AddToBasketAsync(Products product)
    {
        Debug.WriteLine("AddToBasketCommand executed");
        if (product == null)
        {
            return;
        }

        await BasketViewModel.AddToBasket(product);
        await App.Current.MainPage.DisplayAlert("Success", $"{product.Quantity} {product.Name} added to basket!", "OK");
    }
    

    //navigating to the basket page
    [RelayCommand]
    async Task GoToBasket()
    {

        var viewModel = new BasketViewModel(currentUserNumber);
        
        var navigationParameter = new Dictionary<string, object>
        {
            { "BasketViewModel", viewModel }
        };

        await Shell.Current.GoToAsync(nameof(BasketPage), navigationParameter);
    }

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}