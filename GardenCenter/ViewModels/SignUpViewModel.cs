using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GardenCenter.Model;
using System.Diagnostics;
using System.Net.Mail;

namespace GardenCenter.ViewModels;

public partial class SignUpViewModel : ObservableObject
{
    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private bool isCorporate;




    [RelayCommand]
    async Task SignUpAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(PhoneNumber))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Please enter a username and Phone Number.", "OK");
            return;
        }

        //pass the user data to the home page
        var user = new User { Name = username, PhoneNumber = phoneNumber, IsCorporate = isCorporate };

        var users = await App.Database.GetUserAsync();
        foreach (var u in users)
        {
            Debug.WriteLine($"ID: {u.Name}, Username: {u.PhoneNumber}, Email: {u.IsCorporate}");

            if (user.PhoneNumber == u.PhoneNumber)
            {
                await App.Current.MainPage.DisplayAlert("Error", "This Phone Number Already Has an Account", "OK");
                return;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Success", "You have successfully signed up.", "OK");

                try
                {
                    await Shell.Current.GoToAsync($"///LoginPage", true,
                                                    new Dictionary<string, object>
                                                    {
                                                { "User", user }
                                                    });

                    await App.Database.SaveUserAsync(user);

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }
    [RelayCommand]
    async Task CancelAsync()
    {
        await Shell.Current.GoToAsync($"///LoginPage", true);
    }

    //clear fields method to be used when page opens
    public void ClearFields()
    {
        Username = string.Empty;
        PhoneNumber = string.Empty;
        IsCorporate = false;
    }
}