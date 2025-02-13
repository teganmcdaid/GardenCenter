
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GardenCenter.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GardenCenter.ViewModels
{
    public partial class BasketViewModel : BaseViewModel
    {
        // Create Basket list
        public ObservableCollection<BasketItem> BasketItems { get; set; } = new();

        public string currentUserNumber = Preferences.Get("PhoneNumber", "Unknown");
        public string currentUserName = Preferences.Get("Username", "Unknown");
        public bool currentCorporateStatus = Preferences.Get("IsCorporate", false);

        private string phoneNum;
        public BasketViewModel(string phoneNumber)
        {
            phoneNum = phoneNumber;
            LoadBasketItems();
        }

        public async Task LoadBasketItems()
        {
            var items = await App.Database.GetUserBasketAsync(phoneNum);
            BasketItems.Clear();
            foreach (var item in items)
            {
                BasketItems.Add(item);
            }
        }

        // Add product to basket
        public async Task AddToBasket(Products product)
        {
            // Check if the item is already in the basket
            var existingItem = BasketItems.FirstOrDefault(i => i.ProductName == product.Name);

            // If it does exist, update quantity
            if (existingItem != null)
            {
                existingItem.Quantity += product.Quantity;
                await App.Database.UpdateBasketItemAsync(existingItem);
            }
            else
            {
                // If it does not exist, add new item
                var basketItem = new BasketItem
                {
                    PhoneNumber = phoneNum,
                    Username = currentUserName,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    IsCorporate = currentCorporateStatus
                };
                await App.Database.AddToBasketAsync(basketItem);
            }
        }

        // Remove product from basket
        [RelayCommand]
        public async Task RemoveFromBasket(BasketItem item)
        {
            if (item != null)
            {
                BasketItems.Remove(item);
                await App.Database.RemoveFromBasketAsync(item.ItemId);
            }
        }

        // Clear basket when checkout is complete
        public async Task ClearBasket()
        {
            await App.Database.ClearBasketAsync(phoneNum);
        }

        public async Task Checkout()
        {
            if(currentCorporateStatus == false)
            {
                ClearBasket();
                await App.Current.MainPage.DisplayAlert("Success", "Your Purchase was Succesful.", "OK");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Success", "Your Purchase has been added to your Monthly bill.", "OK");

            }
        }
    }
}
