using GardenCenter.Services;
using GardenCenter.ViewModels;

namespace GardenCenter.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		BindingContext = new HomePageViewModel(new ProductService());
	}
}