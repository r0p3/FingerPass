using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using r0p3_Password_Manager.Models;
using r0p3_Password_Manager.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace r0p3_Password_Manager.ViewModels
{
    [QueryProperty(nameof(Password), nameof(Password))]
    [QueryProperty(nameof(ID), nameof(ID))]
    class AddNewPasswordViewModel : BindableObject
    {

        private Password fullPassword;
        public Password FullPassword
        {
            get => fullPassword;
            set
            {
                fullPassword = value;
                
            }
        }

        private string service = "";
        public string Service { get => service; set { service = value; updateCanSavePassowrd(); } }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        private string password = "";
        public string Password { get => password; set { password = Uri.UnescapeDataString(value); updateCanSavePassowrd(); OnPropertyChanged(); } }

        public bool canSavePassword { get; set; } = false;

        public bool Favorite { get; set; }

        private int id;
        public int ID
        {
            get => id;
            set
            {
                id = value;
                getPassword(value);
                editMode = true;
            }
        }
        private bool editMode = false;

        public ICommand generatePassword { get; set; }
        public ICommand savePassword { get; set; }
        public ICommand GeneratorSettings { get; set; }

        private async void getPassword(int id)
        {
            fullPassword = await PasswordStorage.getPassword(id);
            Service = fullPassword.Service;
            Username = fullPassword.Username;
            Email = fullPassword.Email;
            Details = fullPassword.Details;
            Password = new Encryption().decrypt(fullPassword.PasswordEncrypted, await SecureStorage.GetAsync("PasswordEncryptor"));
            Favorite = fullPassword.Favorite;
            updateAll();
        }

        public AddNewPasswordViewModel()
        {
            BindingContext = this;
            generatePassword = new Command(generateNewPassword);
            savePassword = new Command(saveNewPassword);
            GeneratorSettings = new Command(generatorSettings);
        }

        public void generateNewPassword()
        {
            Password = PasswordGenerator.generate(
                Preferences.Get(Constants.KEY_LENGTH, 16),
                Preferences.Get(Constants.KEY_LOWERCASE, true),
                Preferences.Get(Constants.KEY_UPPERCASE,true),
                Preferences.Get(Constants.KEY_NUMBERS, true),
                Preferences.Get(Constants.KEY_SYMBOLS, true));
            OnPropertyChanged(nameof(Password));
        }

        public async void generatorSettings()
        {
            await Shell.Current.GoToAsync($"{nameof(GeneratorSettingsPage)}");
        }

        public async void saveNewPassword()
        {
            fullPassword = new Password()
            {
                Service = Service,
                Username = Username,
                Email = Email,
                Details = Details,
                PasswordEncrypted = new Encryption().encrypt(Password, await SecureStorage.GetAsync("PasswordEncryptor")),
                Favorite = Favorite
            };
            if (editMode)
            {
                fullPassword.ID = id;
                await PasswordStorage.updatePassword(FullPassword);
            }
            else
                await PasswordStorage.addPassword(fullPassword);
            //await App.Current.MainPage.DisplayAlert(Service, "was saved", "Continue");
            await Shell.Current.GoToAsync($"{nameof(PasswordsPage)}?Update={true}");
            //var route = $"{nameof(PasswordsPage)}?{nameof(PasswordsViewModel.Update)}={true}";
            
            //await Shell.Current.GoToAsync(route);
        }

        private void updateCanSavePassowrd()
        {
            canSavePassword = password.Length > 0 && service.Length > 0;
            OnPropertyChanged(nameof(canSavePassword));
        }

        private void updateAll()
        {
            OnPropertyChanged(nameof(Service));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Details));
            OnPropertyChanged(nameof(Password));
        }
    }
}
