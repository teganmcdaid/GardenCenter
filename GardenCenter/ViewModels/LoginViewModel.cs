using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GardenCenter.Model;

namespace GardenCenter.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string emailAddress;

    [RelayCommand]
    async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(EmailAddress))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Please enter a username and email address.", "OK");
            return;
        }

        else
        {
            await App.Current.MainPage.DisplayAlert("Success", "You have successfully logged in.", "OK");
        }

        //pass the user data to the home page
        var user = new User { Name = username, Email = emailAddress };

        //check if user is admin if is pass to admin page
        if (Username == "Admin" && EmailAddress == "Admin@Admin")
        {
            try
            {
                await Shell.Current.GoToAsync($"///AdminPage", true,
                                                new Dictionary<string, object>
                                                {
                                                    { "User", user }
                                                });
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        else
        {
            try
            {
                await Shell.Current.GoToAsync($"///HomePage", true,
                                                new Dictionary<string, object>
                                                {
                                                { "User", user }
                                                });
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}


