using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GardenCenter.Model;
using GardenCenter.Views;
using System.Collections.ObjectModel;

namespace GardenCenter.ViewModels;
public partial class ProductsViewModel : BaseViewModel
{

	//Products list to store products being passed through
	public ObservableCollection<Products> Products { get; set; } = new();
	public ProductsViewModel(){	}

    public ProductsViewModel(List<Products> products)
    {
        Products = new ObservableCollection<Products>(products);
    }

    [RelayCommand]
    async Task Back()
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }
}