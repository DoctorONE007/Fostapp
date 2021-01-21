using ShoppingCarts.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCarts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage, ILoginPage
    {
        private LoginViewModel viewModels;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModels = new LoginViewModel(this);
        }

        public void NavigateToMainPage()
        {
            //await Navigation.PushModalAsync(new MainPage());
            App.Current.MainPage = new MainPage();
        }

        public void NavigateToRegisterPage()
        {
            App.Current.MainPage = new RegisterPage();
        }

        public void ShowError(string message)
        {
            DisplayAlert("Ошибка", message, "OK");
        }
    }
}