using MvvmHelpers;
using ShoppingCarts.Services.ServiceInterface;
using ShoppingCarts.Views;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ShoppingCarts.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Fields
        private static string EMAIL_REGEX = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}";
        private static string PHONE_REGEX = @"^((\+7|8)+([0-9]){10})$";
        private IRegisterPage view;
        private IUserService userService;
        private string firstName;
        private string lastName;
        private string middleName;
        private string phone;
        private string email;
        private string password;
        private string passwordAgain;
        #endregion

        #region Properties
        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { SetProperty(ref middleName, value); }
        }

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

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

        public string PasswordAgain
        {
            get { return passwordAgain; }
            set { SetProperty(ref passwordAgain, value); }
        }

        public Command RegisterCommand { get; set; }
        public Command LoginCommand { get; set; }
        #endregion

        #region ctor
        public RegisterViewModel(IRegisterPage view)
        {
            this.view = view;
            userService = DependencyService.Get<IUserService>();
            RegisterCommand = new Command(() => OnRegister());
            LoginCommand = new Command(() => OnLogin());
        }
        #endregion

        #region Commands
        private async void OnRegister()
        {
            if (!CheckEmail(Email)  || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(MiddleName) || string.IsNullOrEmpty(LastName) || !CheckPhone(Phone) || !Password.Equals(PasswordAgain))
            {
                view.ShowError("Не корректно заполнены некоторые поля.");
                return;
            }

            IsBusy = true;
            var result = await userService.RegisterAsync(FirstName, MiddleName, LastName, Email, Phone, Password);
            if (!result.IsFaulted)
                view.NavigateToMainPage();
            else
                view.ShowError(result.Exception.Message);
            IsBusy = false;
        }

        private void OnLogin()
        {
            view.NavigateToLoginPage();
        }
        #endregion

        #region Functions
        private bool CheckEmail(string email)
        {
            Regex regex = new Regex(EMAIL_REGEX);
            var matches = regex.Matches(email);
            return matches.Count != 0;
        }

        private bool CheckPhone(string phone)
        {
            Regex regex = new Regex(PHONE_REGEX);
            var matches = regex.Matches(phone);
            return matches.Count != 0;
        }
        #endregion
    }
}
