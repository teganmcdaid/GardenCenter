using GardenCenter.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GardenCenter.ViewModels;

public partial class CorporateViewModel : BaseViewModel
{
    //instatiate corporate items list
    public ObservableCollection<Corporate> CorporateItems { get; set; } = new();
    private string phoneNum;
    public string currentUserName = Preferences.Get("Username", "Unknown");

    public CorporateViewModel() { }
    public CorporateViewModel(string phoneNumber)
    {
        phoneNum = phoneNumber;
        LoadCorporateItems();
    }

    //load items from db
    public async Task LoadCorporateItems()
    {
        //call db to get corporate basket
        var items = await App.Database.GetCorporateBasketAsync(phoneNum);
        CorporateItems.Clear();
        foreach (var item in items)
        {
            //add each item to corporate items list
            CorporateItems.Add(item);
        }
        //update toolbar 
        OnPropertyChanged(nameof(TotalBasketPrice));
        OnPropertyChanged(nameof(TotalBasketQuantity));

    }

    //get price of everything in the basket
    public double TotalBasketPrice => CorporateItems.Sum(item => item.TotalItemPrice);

    //get number of items in the basket
    public int TotalBasketQuantity => CorporateItems.Sum(item => item.Quantity);

    //add items to corporate account once checked out in basket view model
    public async Task AddToAccount(BasketItem product)
    {
        
        // Check if the item is already in the basket
        var existingItem = CorporateItems.FirstOrDefault(i => i.ProductName == product.ProductName);

        // If it does exist, update quantity
        if (existingItem != null)
        {
            existingItem.Quantity += product.Quantity;
            existingItem.TotalItemPrice = product.Price * existingItem.Quantity;
            await App.Database.UpdateCorporateItemAsync(existingItem);
        }
        else
        {
            // If it does not exist, add new item
            var corporateItem = new Corporate
            {
                PhoneNumber = phoneNum,
                Name = currentUserName,
                ProductName = product.ProductName,
                Quantity = product.Quantity,
                TotalItemPrice = (product.Price * product.Quantity)
            };
            CorporateItems.Add(corporateItem);
            await App.Database.AddToCorporateBillAsync(corporateItem);
         }
         //refresh the basket
         await LoadCorporateItems();
    }

}