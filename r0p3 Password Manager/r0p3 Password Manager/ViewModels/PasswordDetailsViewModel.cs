using r0p3_Password_Manager.Models;
using r0p3_Password_Manager.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace r0p3_Password_Manager.ViewModels
{
    [QueryProperty(nameof(ID), nameof(ID))]
    class PasswordDetailsViewModel : BindableObject
    {
        public ICommand Delete { get; set; }
        public ICommand Edit { get; set; }
        public PasswordDetailsViewModel()
        {
            BindingContext = this;
            Delete = new Command(onClickDelete);
            Edit = new Command(onClickEdit);
        }
        private int id;
        public int ID 
        {
            get => id;
            set
            {
                id = value;
                getPassword(value);
                OnPropertyChanged();
                
            }
        }
        private async void getPassword(int id)
        {
            var password = await PasswordStorage.getPassword(id);
            password.PasswordEncrypted = new Encryption().decrypt(password.PasswordEncrypted, await SecureStorage.GetAsync("PasswordEncryptor"));
            PasswordDetails = password;
        }
        private Password passwordDetails;
        public Password PasswordDetails
        {
            get => passwordDetails;
            set
            {
                //value.PasswordEncrypted = decryptPassword(value.PasswordEncrypted).GetAwaiter().GetResult();
                passwordDetails = value;
                OnPropertyChanged();
            }
        }

        private async void onClickEdit()
        {
            var route = $"{nameof(AddNewPasswordPage)}?ID={ID}";
            await Shell.Current.GoToAsync(route);
        }

        private async void onClickDelete()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Delete", "Are you sure you want to delete this password?", "Yes", "No");
            if(answer)
            {
                await PasswordStorage.removePassword(ID);
            }
            await Shell.Current.GoToAsync($"..?Update={true}");
        }
    }
}
