using MvvmHelpers;
using ShoppingCarts.Services.ServiceInterface;
using ShoppingCarts.Views;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ShoppingCarts.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private static string PHONE_REGEX = @"^((\+7|8)+([0-9]){10})$";
        private ILoginPage view;
        private IUserService userService;
        private string phone;
        private string password;
        #endregion

        #region Properties
        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        #endregion

        #region ctor
        public LoginViewModel(ILoginPage view)
        {
            this.view = view;
            userService = DependencyService.Get<IUserService>();
            LoginCommand = new Command(() => OnLogin());
            RegisterCommand = new Command(() => OnRegister());
        }
        #endregion

        #region Commands
        private async void OnLogin()
        {
            if (!CheckPhone(Phone))
            {
                view.ShowError("Не корректный номер телефона");
                return;
            }

            IsBusy = true;
            var result = await userService.LoginAsync(Phone, Password);
            if (!result.IsFaulted && result.Value != null)
                view.NavigateToMainPage();
            else
                view.ShowError(result.Exception.Message);
            IsBusy = false;
        }

        private void OnRegister()
        {
            view.NavigateToRegisterPage();
        }
        #endregion

        #region Functions
        private bool CheckPhone(string phone)
        {
            Regex regex = new Regex(PHONE_REGEX);
            var matches = regex.Matches(phone);
            return matches.Count != 0;
        }
        #endregion
    }
}
