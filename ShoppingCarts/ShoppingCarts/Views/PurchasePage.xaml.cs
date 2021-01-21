using ShoppingCarts.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCarts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchasePage : ContentPage, IPurchasePage
    {
        private PurchaseViewModel viewModel;
        public PurchasePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PurchaseViewModel(this);
        }

        public async void Back()
        {
            await Navigation.PopAsync();
        }

        public void ShowMessage(string title, string message)
        {
            DisplayAlert(title, message, "OK");
        }
    }
}