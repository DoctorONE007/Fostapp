namespace ShoppingCarts.Views
{
    public interface IRegisterPage
    {
        void NavigateToLoginPage();
        void NavigateToMainPage();
        void ShowError(string message);
    }
}
