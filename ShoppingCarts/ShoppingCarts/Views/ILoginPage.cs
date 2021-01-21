namespace ShoppingCarts.Views
{
    public interface ILoginPage
    {
        void NavigateToMainPage();
        void NavigateToRegisterPage();
        void ShowError(string message);
    }
}
