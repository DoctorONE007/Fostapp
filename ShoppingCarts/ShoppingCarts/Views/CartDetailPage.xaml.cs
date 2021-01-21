using ShoppingCarts.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCarts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartDetailPage : ContentPage, ICartDetailPage
    {
        public CartDetailPageViewModel viewModel;

        public CartDetailPage()
        {
            InitializeComponent();
            
            BindingContext = viewModel = new CartDetailPageViewModel(this);
        }

        public async Task NavigateToPurchasePage()
        {
            await Navigation.PushAsync(new PurchasePage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetData.Execute(null);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}