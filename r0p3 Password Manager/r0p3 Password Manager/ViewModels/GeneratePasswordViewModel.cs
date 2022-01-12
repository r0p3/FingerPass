using r0p3_Password_Manager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace r0p3_Password_Manager.ViewModels
{
    [QueryProperty(nameof(Password), nameof(Password))]
    class GeneratePasswordViewModel :BindableObject
    {
        private int passwordLength = 16;
        public int PasswordLength { get { return passwordLength; } set { passwordLength = value; OnPropertyChanged(nameof(PasswordLength)); } }
        public bool Uppercase { get; set; } = true;
        public bool Lowercase { get; set; } = true;
        public bool Numbers { get; set; } = true;
        public bool Symbols { get; set; } = true;

        string password;
        public string Password {
            get => password;
            set
            {
                password = Uri.UnescapeDataString(value);
                OnPropertyChanged();
            }
        }

        public ICommand GeneratePassword { get; set; }
        public ICommand SavePassword { get; set; }

        public GeneratePasswordViewModel()
        {
            BindingContext = this;
            GeneratePassword = new Command(generatePassword);
            SavePassword = new Command(savePassword);
        }

        private void generatePassword()
        {
            Password = PasswordGenerator.generate(PasswordLength, Lowercase, Uppercase, Numbers, Symbols);
            OnPropertyChanged(nameof(Password));
        }

        private async void savePassword()
        {
            await Shell.Current.GoToAsync($"..?{nameof(Password)}={Uri.EscapeDataString(Password)}");
        }
    }
}
