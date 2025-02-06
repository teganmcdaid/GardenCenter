using GardenCenter.ViewModels;

namespace GardenCenter.Views;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
        BindingContext = new SignUpViewModel();
    }

    //used to clear the fields when page is first opened
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SignUpViewModel viewModel)
        {
            viewModel.ClearFields();
        }
    }
}