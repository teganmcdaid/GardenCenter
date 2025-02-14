using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GardenCenter.Model;
using System.Diagnostics;

namespace GardenCenter.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string phoneNumber;


    [RelayCommand]
    async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(PhoneNumber))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Please enter a username and PhoneNumber.", "OK");
            return;
        }


        //pass the user data to the home page
        var user = new User { Name = username, PhoneNumber = phoneNumber};

        //getlist of users from databae
        var users = await App.Database.GetUserAsync();

        //set default values to false only change when these are true
        bool registeredUser = false;
        bool incorrectName = false;

        //iterate through users
        foreach (var u in users)
        {
            //check if user is stored in database
            if (user.PhoneNumber == u.PhoneNumber &&
                user.Name == u.Name)
            {
                //set registered user to true if user is found in database
                registeredUser = true;

                //user is equal to the stored user which has correct corporate status
                user.IsCorporate = u.IsCorporate;
                await App.Current.MainPage.DisplayAlert("Success", "You have successfully logged in.", "OK");

                try
                {

                    //navigate to HomePage
                    await Shell.Current.GoToAsync($"///HomePage", true,
                                                    new Dictionary<string, object>
                                                    {
                                                { "User", user }
                                                    });
                    //clear prefrences incase previosuly set
                    Preferences.Default.Clear();
                    //set preferences so user will be accessible
                    Preferences.Default.Set("PhoneNumber", user.PhoneNumber);
                    Preferences.Default.Set("Username", user.Name);
                    Preferences.Default.Set("IsCorporate", user.IsCorporate);
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }

            }
            //If Number is in Database and name is not inform user that name does not match number
            else if (user.PhoneNumber == u.PhoneNumber && user.Name != u.Name)
                incorrectName = true;
        }

        //inform user that username is incorrect, number is already registered
        if (incorrectName == true)
            await App.Current.MainPage.DisplayAlert("Error", "Username Incorrect.", "OK");

        //inform user that the account is not found and they need to sign up
        else if (registeredUser == false && incorrectName == false)
            await App.Current.MainPage.DisplayAlert("Error", "Account not found. Please sign up", "OK");



    }



    [RelayCommand]
    async Task SignUpAsync()
    {
        try
        {
            await Shell.Current.GoToAsync($"///SignUp", true);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }


    //clear fields when page opens when sign up is cancelled
    public void ClearFields()
    {
        Username = string.Empty;
        PhoneNumber = string.Empty;
    }
}



