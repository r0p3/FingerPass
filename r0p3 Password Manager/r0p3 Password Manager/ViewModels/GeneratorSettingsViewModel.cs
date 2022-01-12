using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using r0p3_Password_Manager.Models;

namespace r0p3_Password_Manager.ViewModels
{
    class GeneratorSettingsViewModel : BindableObject
    {
        private int length;
        private bool uppercase, lowercase, numbers, symbols;


        public int Length 
        {
            get => length;
            set
            {
                length = value;
                Preferences.Set(Constants.KEY_LENGTH, length);
                OnPropertyChanged();
            }
        }
        public bool Uppercase 
        { 
            get => uppercase;
            set
            {
                uppercase = value;
                Preferences.Set(Constants.KEY_UPPERCASE, uppercase);
                OnPropertyChanged();
            }
        }
        public bool Lowercase 
        {
            get => lowercase;
            set
            {
                lowercase = value;
                Preferences.Set(Constants.KEY_LOWERCASE, lowercase);
                OnPropertyChanged();
            }
        }
        public bool Numbers 
        {
            get => numbers;
            set
            {
                numbers = value;
                Preferences.Set(Constants.KEY_NUMBERS, numbers);
                OnPropertyChanged();
            }
        }
        public bool Symbols 
        {
            get => symbols;
            set
            {
                symbols = value;
                Preferences.Set(Constants.KEY_SYMBOLS, symbols);
                OnPropertyChanged();
            }
        }

        public GeneratorSettingsViewModel()
        {
            BindingContext = this;
            loadSettings();
        }

        private void loadSettings()
        {
            Length = Preferences.Get(Constants.KEY_LENGTH, 16);
            Uppercase = Preferences.Get(Constants.KEY_UPPERCASE, true);
            Lowercase = Preferences.Get(Constants.KEY_LOWERCASE, true);
            Numbers = Preferences.Get(Constants.KEY_NUMBERS, true);
            Symbols = Preferences.Get(Constants.KEY_SYMBOLS, true);
        }
    }
}
