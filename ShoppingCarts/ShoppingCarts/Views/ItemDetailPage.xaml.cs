using MongoDB.Bson;
using ShoppingCarts.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCarts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        private ItemDetailPageViewModel _viewModel;

        public ItemDetailPage(ObjectId id)
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemDetailPageViewModel(id);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.GetData();
        }
    }
}