using GardenCenter.ViewModels;

namespace GardenCenter.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }

    //used to clear the fields when page is first opened
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is LoginViewModel viewModel)
        {
            viewModel.ClearFields();
        }
    }
}