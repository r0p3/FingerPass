using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace r0p3_Password_Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailItemControl : ContentView
    {
        public static readonly BindableProperty TitleTextProperty = 
            BindableProperty.Create(
                nameof(TitleText),
                typeof(string),
                typeof(DetailItemControl),
                defaultValue:string.Empty,
                propertyChanged: TitleTextPropertyChanged);

        public static readonly BindableProperty DetailTextProperty =
            BindableProperty.Create(
                nameof(TitleText),
                typeof(string),
                typeof(DetailItemControl),
                defaultValue: string.Empty,
                propertyChanged: DetailTextPropertyChanged);

        private static void DetailTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (DetailItemControl)bindable;
            control.Detail.Text = newValue?.ToString();
        }

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (DetailItemControl)bindable;
            control.Title.Text = newValue?.ToString();
        }

        public string TitleText 
        {
            get => base.GetValue(TitleTextProperty)?.ToString();
            set => base.SetValue(TitleTextProperty, value);
        }
        public string DetailText 
        {
            get => base.GetValue(DetailTextProperty)?.ToString();
            set => base.SetValue(DetailTextProperty, value);
        }
        public DetailItemControl()
        {
            InitializeComponent();
            BtnCopy.Clicked += BtnCopy_Clicked;
        }

        private async void BtnCopy_Clicked(object sender, EventArgs e)
        {
            if (DetailText.Length > 0)
            {
                await Clipboard.SetTextAsync(DetailText);
                Acr.UserDialogs.UserDialogs.Instance.Toast($"Copied {DetailText} to clipboard", new TimeSpan(3));
            }
        }
    }
}