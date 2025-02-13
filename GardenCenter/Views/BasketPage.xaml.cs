using GardenCenter.ViewModels;

namespace GardenCenter.Views;

//need to use query attribute to pass the property to products view model
[QueryProperty(nameof(BasketViewModel), "BasketViewModel")]
public partial class BasketPage : ContentPage
{
    //need parameterless constructor for nevigating
    public BasketPage()
    {
        InitializeComponent();
    }
    public BasketViewModel BasketViewModel
    {
        set
        {
            BindingContext = value;
        }
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}