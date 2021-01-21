using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmHelpers;
using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using Xamarin.Forms;
using System.Linq;
using ShoppingCarts.Translators;

namespace ShoppingCarts.ViewModels
{
    public class CartPageViewModel : BaseViewModel
    {
        private List<Item> products;
        private ObservableRangeCollection<CartItemModel> items;

        private IReadOnlyDictionary<Item, int> cart;

        public ObservableRangeCollection<CartItemModel> Items { get => items; set
            {
                SetProperty(ref items, value);
            }
        }

        public IReadOnlyDictionary<Item, int> Cart { get => cart; set
            {
                SetProperty(ref cart, value);
            }
        }

        public ObservableRangeCollection<Grouping<string, CartItemModel>> ShoppingItemsGrouped { get; set; } = new ObservableRangeCollection<Grouping<string, CartItemModel>>();


        public string cartCounter;

        public string CartCounter
        {
            get { return cartCounter; }
            set { SetProperty(ref cartCounter, value); }
        }

        public string buttonText;

        public string ButtonText
        {
            get { return buttonText; }
            set { SetProperty(ref buttonText, value); }
        }

        public Command GetData { get; set; }

        public Command OnItemButtonClickedCommand { get; set; }

        public readonly IItemService itemsService;
        public readonly ICartService cartService;

        public CartPageViewModel()
        {
            Items = new ObservableRangeCollection<CartItemModel>();
            GetData = new Command(async () => await GetDataCommand());
            itemsService = DependencyService.Get<IItemService>();
            cartService = DependencyService.Get<ICartService>();
            Cart = cartService.GetItems();
            OnItemButtonClickedCommand = new Command<CartItemModel>((e) => ExecuteButtonClick(e));
        }

        private void ExecuteButtonClick(CartItemModel item)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (item.IsInCart)
                {
                    cartService.RemoveById(item.Id);
                    item.IsInCart = false;
                }
                else
                {
                    var cartItem = products.FirstOrDefault(i => i.Id == item.Id);
                    if (cartItem != null) {
                        cartService.Add(cartItem);
                        item.IsInCart = true;
                    }
                }

                UpdateCartCounter();
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

                products = (await itemsService.GetItemsAsync()).ToList();

                List<CartItemModel> ItemsList = products
                    .Select(i => i.ToCartItemModel(cartService.GetItems()))
                    .ToList();
            
                Items.Clear();

                Items.ReplaceRange(ItemsList);

                ShoppingItemsGrouped = new ObservableRangeCollection<Grouping<string, CartItemModel>>(GroupItems(Items));

                UpdateCartCounter();
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

        private void UpdateCartCounter()
        {
            CartCounter = Items.Count(i => i.IsInCart).ToString();
        }

        private IEnumerable<Grouping<string, CartItemModel>> GroupItems(ObservableRangeCollection<CartItemModel> ShoppingItems)
        {
            var sorted = ShoppingItems.OrderBy(i => i.Name)
                .GroupBy(i => i.NameSort)
                .Select(i => new Grouping<string, CartItemModel>(i.Key, i));

            return sorted;
        }
    }
}