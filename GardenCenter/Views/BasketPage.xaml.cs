using GardenCenter.Model;
using GardenCenter.ViewModels;
using System.Diagnostics;

namespace GardenCenter.Views;

//need to use query attribute to pass the property to products view model
[QueryProperty(nameof(BasketViewModel), "BasketViewModel")]
public partial class BasketPage : ContentPage
{
    //need parameterless constructor for nevigating
    public BasketPage()
    {
        InitializeComponent();
        BindingContext = new BasketViewModel();
    }
    public BasketViewModel BasketViewModel
    {
        set
        {
            BindingContext = value;
           // Debug.WriteLine($"ViewModel is set: {BindingContext?.GetType().Name}");

        }
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {

    }
}