using MongoDB.Bson;
using MvvmHelpers;
using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using System;
using Xamarin.Forms;

namespace ShoppingCarts.ViewModels
{
    public class ItemDetailPageViewModel : BaseViewModel
    {
        #region Fields
        private Item item;
        private ObjectId id;

        private string imageSource;
        private string itemName;
        private string description;
        private bool isInCart;

        private readonly IItemService itemsService;
        private readonly ICartService cartService;
        #endregion

        #region Properties
        public string ImageSource
        {
            get { return imageSource; }
            set { SetProperty(ref imageSource, value); }
        }

        public string ItemName
        {
            get { return itemName; }
            set { SetProperty(ref itemName, value); }
        }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public bool IsInCart
        {
            get { return isInCart; }
            set { SetProperty(ref isInCart, value); }
        }

        public Command ButtonClicked { get; set; }
        #endregion

        public ItemDetailPageViewModel(ObjectId id)
        {
            this.id = id;
            itemsService = DependencyService.Get<IItemService>();
            cartService = DependencyService.Get<ICartService>();

            ButtonClicked = new Command(() => OnButtonClickedCommand(item));
        }

        public async void GetData()
        {
            item = await itemsService.GetItemAsync(id);
            ImageSource = item.Image;
            ItemName = item.Name;
            Description = item.Description;
            IsInCart = cartService.GetItems().ContainsKey(item);
        }

        private void OnButtonClickedCommand(Item ShoppingItem)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (cartService.GetItems().ContainsKey(item))
                {
                    cartService.RemoveById(item.Id);
                    IsInCart = false;
                } else
                {
                    cartService.Add(item);
                    IsInCart = true;
                }
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
    }
}