using ShoppingCarts.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCarts.ContentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatInputBarView : Xamarin.Forms.ContentView
    {
        public ChatInputBarView()
        {
            InitializeComponent();

           
        }

        public void Handle_Completed(object sender, EventArgs e)
        {
            (this.Parent.Parent.BindingContext as ChatPageViewModel).OnSendCommand.Execute(null);
            chatTextInput.Focus();
        }

        public void UnFocusEntry()
        {
            chatTextInput?.Unfocus();
        }
    }
}