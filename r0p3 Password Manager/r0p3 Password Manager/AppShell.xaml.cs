using System;
using System.Collections.Generic;

using Xamarin.Forms;
using r0p3_Password_Manager.Views;

namespace r0p3_Password_Manager
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PasswordsPage), typeof(PasswordsPage));
            Routing.RegisterRoute(nameof(PasswordDetailsPage), typeof(PasswordDetailsPage));
            Routing.RegisterRoute(nameof(AddNewPasswordPage), typeof(AddNewPasswordPage));
            Routing.RegisterRoute(nameof(GeneratePasswordPage), typeof(GeneratePasswordPage));
            Routing.RegisterRoute(nameof(GeneratorSettingsPage), typeof(GeneratorSettingsPage));
        }
    }
}
