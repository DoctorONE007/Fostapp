using System;
using MvvmHelpers;
using ShoppingCarts.Services.ServiceInterface;
using ShoppingCarts.Storage;
using ShoppingCarts.Views;
using Xamarin.Forms;
using System.Linq;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCarts.ViewModels
{
    public class PurchaseViewModel : BaseViewModel
    {
        #region Fields
        private IPurchasePage view;
        private IOrderService orderService;
        private ICartService cartService;
        private IUserService userService;
        private IItemService itemService;
        private IItemStorage itemStorage;
        private string train;
        private int carriage;
        private int place;
        #endregion

        #region Properties
        public string Train
        {
            get { return train; }
            set { SetProperty(ref train, value); }
        }

        public int Carriage
        {
            get { return carriage; }
            set { SetProperty(ref carriage, value); }
        }

        public int Place
        {
            get { return place; }
            set { SetProperty(ref place, value); }
        }

        public Command PurchaseCommand { get; set; }
        #endregion

        #region ctor
        public PurchaseViewModel(IPurchasePage view)
        {
            this.view = view;
            orderService = DependencyService.Get<IOrderService>();
            cartService = DependencyService.Get<ICartService>();
            userService = DependencyService.Get<IUserService>();
            itemService = DependencyService.Get<IItemService>();
            itemStorage = DependencyService.Get<IItemStorage>();
            PurchaseCommand = new Command(() => OnPurchaseCommand());
        }
        #endregion

        #region Commands
        private async void OnPurchaseCommand()
        {
            if (string.IsNullOrEmpty(Train) || Place == 0 || Carriage == 0)
            {
                view.ShowMessage("Ошибка", "Заполните все поля!");
                return;
            }

            try
            {
                var result = await CheckItemsBalance();
                if (result)
                {
                    await UpdateItems();
                    IsBusy = true;
                    await orderService.CreateAsync(userService.GetCurrentUser().Value, Train, Carriage, Place, cartService.GetItems());
                    cartService.Clear();
                    await itemService.UpdateItemsAsync();
                    view.ShowMessage("Спасибо", "Ваш заказ успешно размещен.");
                    view.Back();
                }
                else
                {
                    view.Back();
                }
            }
            catch(Exception e)
            {
                view.ShowMessage("Ошибка", e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Functions
        private async Task UpdateItems()
        {
            try
            {
                IsBusy = true;
                foreach (var item in cartService.GetItems().Keys)
                {
                    await itemStorage.ChangeItemAsync(item.Id, cartService.GetItems()[item]);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<bool> CheckItemsBalance()
        {
            IsBusy = true;
            try
            {
                var result = await itemStorage.GetItemsAsync();
                var notExists = new List<ObjectId>();
                if (!result.IsFaulted)
                {
                    foreach (var cartItemKey in cartService.GetItems().Keys)
                    {
                        var item = result.Value.FirstOrDefault(i => i.Id == cartItemKey.Id);
                        if (item == null || item.Balance < cartService.GetItems()[cartItemKey])
                        {
                            notExists.Add(cartItemKey.Id);
                        }
                    }
                    if (notExists.Count > 0)
                    {
                        foreach (var id in notExists)
                        {
                            cartService.RemoveById(id);
                        }
                        await itemService.UpdateItemsAsync();
                        view.ShowMessage("Внимание", "Некоторые выбранные блюда более недоступны для заказа, они были удалены из заказа, проверьте содержимое корзины и выберите замену.");
                        return false;
                    } else
                    {
                        return true;
                    }
                } else
                {
                    view.ShowMessage("Ошибка", result.Exception.Message);
                    return false;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
