using CommunityToolkit.Mvvm.ComponentModel;
using GardenCenter.ViewModels;
using SQLite;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace GardenCenter.Model;

public class BasketItem : INotifyPropertyChanged
{
		[PrimaryKey, AutoIncrement]
		public int ItemId {get; set;}
		public string PhoneNumber { get; set; }
		public string Username { get; set; }
		public string ProductName { get; set; }
		public double Price { get; set; }
        public double TotalItemPrice { get; set; }
		//public int Quantity { get; set; }
		public bool IsCorporate { get; set; }


    //Create event to handle the stepper being incremented in the basket
    //this will update the databse and UI as it is changed
    
    private int quantity;
    public int Quantity
    {
        get => quantity;
        set
        {
            if (quantity != value)
            {
                quantity = value;
                TotalItemPrice = quantity * Price;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(TotalItemPrice));
                UpdateDatabase();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
   
    private async void UpdateDatabase()
    {
        await App.Database.UpdateBasketItemAsync(this);
    }


}