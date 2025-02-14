
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GardenCenter.Model;
using GardenCenter.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace GardenCenter.ViewModels
{
    public partial class BasketViewModel : BaseViewModel
    {
        // Create Basket list
        public ObservableCollection<BasketItem> BasketItems { get; set; } = new();

        //get current user info
        public string currentUserNumber = Preferences.Get("PhoneNumber", "Unknown");
        public string currentUserName = Preferences.Get("Username", "Unknown");
        public bool currentCorporateStatus = Preferences.Get("IsCorporate", false);

        //set corporate viewmodel
        public CorporateViewModel CorporateViewModel { get; set; }

        private string phoneNum;

        public BasketViewModel() { }
        public BasketViewModel(string phoneNumber)
        {
            phoneNum = phoneNumber;
            LoadBasketItems();
        }

        //load items in the basket from db
        public async Task LoadBasketItems()
        {
            var items = await App.Database.GetUserBasketAsync(phoneNum);
            BasketItems.Clear();
            foreach (var item in items)
            {
                BasketItems.Add(item);
            }

            //update toolbar 
            OnPropertyChanged(nameof(TotalBasketPrice));
            OnPropertyChanged(nameof(TotalBasketQuantity));
        }

        //get price of everything in the basket
        public double TotalBasketPrice => BasketItems.Sum(item => item.TotalItemPrice);

        //get number of items in the basket
        public int TotalBasketQuantity => BasketItems.Sum(item => item.Quantity);

        // Add product to basket
        public async Task AddToBasket(Products product)
        {
            // Check if the item is already in the basket
            var existingItem = BasketItems.FirstOrDefault(i => i.ProductName == product.Name);

            // If it does exist, update quantity
            if (existingItem != null)
            {
                existingItem.Quantity += product.Quantity;
                existingItem.TotalItemPrice = product.Price * existingItem.Quantity;
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
                    IsCorporate = currentCorporateStatus,
                    TotalItemPrice = (product.Price * product.Quantity)
                };
                //add to basket list and basket database
                BasketItems.Add(basketItem);
                await App.Database.AddToBasketAsync(basketItem);
            }
            //refresh the basket to update it
            await LoadBasketItems();
        }

        // Remove product from basket
        [RelayCommand]
        public async Task RemoveFromBasket(BasketItem item)
        {
            if (item != null)
            {
                //remove item from basket list and db
                BasketItems.Remove(item);
                await App.Database.RemoveFromBasketAsync(item.ItemId);
            }
        }

        // Clear basket when checkout is complete
        public async Task ClearBasket()
        {
            //clear list and db
            BasketItems.Clear();
            await App.Database.ClearBasketAsync(phoneNum);
        }

        //checkout items
        [RelayCommand]
        public async Task CheckoutItems()
        {
            //check if there are any items in the basket
            if (BasketItems.Count == 0)
            {
                await LoadBasketItems();
                await App.Current.MainPage.DisplayAlert("Notice", "Your basket is empty.", "OK");
                return;
            }
            //if user is not corporate
            else if (currentCorporateStatus == false)
            {
                await LoadBasketItems();
                await App.Current.MainPage.DisplayAlert("Success", $"Your Purchase was Successful. Total Price: {TotalBasketPrice:C}", "OK");
                await ClearBasket();

            }
            //if user is corporate
            else
            {
                //instantiate corporate view model
                CorporateViewModel ??= new CorporateViewModel(currentUserNumber);

                foreach (var item in BasketItems.ToList())
                {
                    //add items from checkout to account
                    await CorporateViewModel.AddToAccount(item);
                }
                await ClearBasket();
                await App.Current.MainPage.DisplayAlert("Success", "Your Purchase has been added to your Monthly bill.", "OK");
            }
            //update toolbar
            OnPropertyChanged(nameof(TotalBasketPrice));
            OnPropertyChanged(nameof(TotalBasketQuantity));
        }

        //naviagte to the corporate account page
        [RelayCommand]
        public async Task RetrieveCorporateAccount()
        {
            var viewModel = new CorporateViewModel(currentUserNumber);

            var navigationParameter = new Dictionary<string, object>
        {
            { "CorporateViewModel", viewModel }
        };

            await Shell.Current.GoToAsync(nameof(CorporatePage), navigationParameter);
        }

        //set corporate status for button
        public bool CurrentCorporateStatus
        {
            get => currentCorporateStatus;
            set
            {
                if (currentCorporateStatus != value)
                {
                    currentCorporateStatus = value;
                    OnPropertyChanged(nameof(CurrentCorporateStatus));
                }
            }
        }


    }
}
