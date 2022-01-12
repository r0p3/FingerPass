using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using r0p3_Password_Manager.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using r0p3_Password_Manager.Views;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using System.Linq;

namespace r0p3_Password_Manager.ViewModels
{
    [QueryProperty(nameof(Update), nameof(Update))]
    public class PasswordsViewModel : BindableObject
    {
        public ObservableCollection<Password> Passwords = new ObservableCollection<Password>();
        public ObservableCollection<Password> shownPasswords = new ObservableCollection<Password>();

        string searchstring = "";

        public ICommand updateListView { get; set; }
        public ICommand NavigateNewPassword { get; set; }
        private bool _isRefreshing = false;

        private bool update;
        public bool Update 
        {
            get => update;
            set
            {
                if(value)
                {
                    updatePasswords();
                }
                update = false;
            }
        }

        public PasswordsViewModel()
        {
            updateListView = new Command(updatePasswords);
            NavigateNewPassword = new Command(navigateNewPassword);
            BindingContext = this;
            for (int i = 1 - 1; i >= 0; i--)
            {
                /*passwords.Add(new Password("Facebook", "", "robin.p-96@gmail.com", "password"));
                passwords.Add(new Password("Gmail", "", "email", "password"));
                passwords.Add(new Password("Valorant", "r0p3tv", "robin.pettersson.96@gmail.com", ""));*/
            }

            Task.Run(() => updatePasswords()).Wait();
            //PasswordStorage.removeAllPasswords();
        }

        private async void navigateNewPassword()
        {
            await Shell.Current.GoToAsync(nameof(AddNewPasswordPage));
        }

        public ICommand DeletePassword => new Command<Password>(async(Password password) =>
        {
            if(password != null)
            {
                var request = new AuthenticationRequestConfiguration("r0p3 password manager",$"Are you sure you want to delete {password.Service}?");
                var result = await CrossFingerprint.Current.AuthenticateAsync(request);
                if(result.Authenticated)
                {
                    await PasswordStorage.removePassword(password.ID);
                    updatePasswords();
                }
            }
        });

        public ICommand ToggleFavorite => new Command<Password>(async (Password password) =>
        {
            if (password != null)
            {
                password.Favorite = !password.Favorite;
                await PasswordStorage.updatePassword(password);
                updatePasswords();
            }
        });

        private Password selectedItem;
        public Password SelectedItem 
        {
            get => selectedItem;
            set 
            {
                if(value != null)
                {
                    onItemSelected(value);
                    value = null;
                }
                selectedItem = value;
                OnPropertyChanged();
                
            }
        }
        async void onItemSelected(Password password)
        {
            var request = new AuthenticationRequestConfiguration("FingerPass", "To access more information.");
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            if(result.Authenticated)
            {
                var route = $"{nameof(PasswordDetailsPage)}?ID={password.ID}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
            
            }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public string searchString
        {
            get { return searchstring; }
            set
            {
                shownPasswords.Clear();
                if (value == "")
                {
                    shownPasswords = new ObservableCollection<Password>(Passwords);
                }
                else
                {
                    foreach (var item in Passwords)
                    { 
                        if (item.Service.ToLower().Contains(value) || (item.Username != null && item.Username.ToLower().Contains(value)) || (item.Email != null && item.Email.ToLower().Contains(value)))
                        {
                            shownPasswords.Add(item);
                        }
                    }
                }
            searchstring = value;
            shownpasswords = new ObservableCollection<Password>(shownPasswords);
            }
        }
        public ObservableCollection<Password> passwords
        {
            get { return Passwords; }
            set { Passwords = passwords; }
        }

        public ObservableCollection<Password> shownpasswords
        {
            get { return shownPasswords; }
            set { shownPasswords = value; OnPropertyChanged(nameof(shownpasswords)); }
        }
       

        private async void updatePasswords()
        {
            Passwords.Clear();
            foreach (var item in await PasswordStorage.getPasswords())
            {
                Passwords.Add(item);
            }
            Passwords = new ObservableCollection<Password>(Passwords.OrderByDescending(x => x.Favorite).ThenBy(x => x.Service));
            searchString = searchString;
            IsRefreshing = false;
        }
    }
}
