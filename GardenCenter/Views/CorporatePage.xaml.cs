using GardenCenter.ViewModels;

namespace GardenCenter.Views;

public partial class CorporatePage : ContentPage
{
    private readonly CorporateViewModel _viewModel;

    public CorporatePage()
    {
        InitializeComponent();
        _viewModel = new CorporateViewModel(Preferences.Get("PhoneNumber", "Unknown"));
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCorporateItems(); 
    }
}