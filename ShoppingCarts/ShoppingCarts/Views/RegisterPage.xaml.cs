using ShoppingCarts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCarts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage, IRegisterPage
    {
        private RegisterViewModel viewModels;
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = viewModels = new RegisterViewModel(this);
        }

        public void NavigateToLoginPage()
        {
            App.Current.MainPage = new LoginPage();
        }

        public void NavigateToMainPage()
        {
            App.Current.MainPage = new MainPage();
        }

        public void ShowError(string message)
        {
            DisplayAlert("Ошибка", message, "OK");
        }
    }
}