using Microsoft.AppCenter.Analytics;
using MvvmHelpers;
using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using ShoppingCarts.Translators;
using ShoppingCarts.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingCarts.ViewModels
{
    public class CartDetailPageViewModel : BaseViewModel
    {
        #region Fields
        private ICartDetailPage view;

        private readonly IItemService itemService;
        private readonly ICartService cartService;
        private ObservableCollection<CartItemModel> cart;
        private bool itemsInCart;
        private double cartSum;
        #endregion

        #region Properties
        public ObservableCollection<CartItemModel> Cart
        {
            get => cart;
            set
            {
                SetProperty(ref cart, value);
            }
        }

        public bool ItemsInCart
        {
            get { return itemsInCart; }
            set { SetProperty(ref itemsInCart, value); }
        }

        public double CartSum
        {
            get { return cartSum; }
            set { SetProperty(ref cartSum, value); }
        }

        public Command GetData { get; set; }

        public Command OnItemButtonClickedCommand { get; set; }

        public Command RemoveAllButton { get; set; }

        public Command PurchaseCommand { get; set; }
        #endregion

        #region ctor
        public CartDetailPageViewModel(ICartDetailPage view)
        {
            this.view = view;
            Cart = new ObservableCollection<CartItemModel>();
            GetData = new Command(async () => await GetDataCommand());
            itemService = DependencyService.Get<IItemService>();
            cartService = DependencyService.Get<ICartService>();
            OnItemButtonClickedCommand = new Command<CartItemModel>((e) => ExecuteButtonClick(e));
            RemoveAllButton = new Command(() => RemoveAllItems());
            PurchaseCommand = new Command(() => OnPurchaseCommand());
        } 
        #endregion

        private async void OnPurchaseCommand()
        {
            await view.NavigateToPurchasePage();
        }

        private void RemoveAllItems()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                cartService.Clear();
                Cart.Clear();

                ItemsInCart = false;
                CartSum = 0;

                Analytics.TrackEvent("Remove all items from cart clicked");
            }
            catch (Exception e)
            {
                Console.Write("Exception is " + e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ExecuteButtonClick(CartItemModel item)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                cartService.RemoveById(item.Id);
                Cart.Remove(item);
                UpdateFlags();
                CartSum = Cart.Sum(i => i.Price);                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception is " + ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetDataCommand()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var cart = cartService.GetItems().Keys.Select(i => i.ToCartItemModel(cartService.GetItems()));
                Cart = new ObservableCollection<CartItemModel>(cart);
                CartSum = Cart.Sum(i => i.Price);

                UpdateFlags();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception is : " + e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void UpdateFlags()
        {
            ItemsInCart = Cart.Count > 0;
        }
    }
}