using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace r0p3_Password_Manager.Views.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckboxText : ContentView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(CheckboxText),
                defaultValue: string.Empty,
                propertyChanged: TextChanged);

        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create(
                nameof(CheckBox),
                typeof(bool),
                typeof(CheckboxText),
                defaultValue: true,
                propertyChanged: CheckedChanged);

        private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CheckboxText)bindable;
            control.Text.Text = newValue?.ToString();
        }

        private static void CheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CheckboxText)bindable;
            control.CheckBox.IsChecked = (bool)newValue;
        }

        public string TextProp 
        {
            get => base.GetValue(TextProperty)?.ToString();
            set => base.SetValue(TextProperty, value); 
        }

        public bool CheckedProp 
        { 
            get => bool.Parse(base.GetValue(CheckedProperty)?.ToString());
            set => base.SetValue(CheckedProperty, value);
        }
        public CheckboxText()
        {
            InitializeComponent();
            CheckBox.CheckedChanged += CheckBox_CheckedChanged;
        }

        public void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            
        }
    }
}