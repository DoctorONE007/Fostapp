using MongoDB.Bson;
using MvvmHelpers;

namespace ShoppingCarts.Model
{
    public class CartItemModel : ObservableObject
    {
        #region Fields
        private string image;
        private string name;
        private string description;
        private string shortDescription;
        private bool isInCart;
        private ObjectId id;
        private double price;
        #endregion

        #region Properties
        public string Image
        {
            get => image;
            set
            {
                SetProperty(ref image, value);
            }
        }

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }

        public bool IsInCart
        {
            get => isInCart;
            set
            {
                SetProperty(ref isInCart, value);
            }
        }

        public ObjectId Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
            }
        }

        public string Description
        {
            get => description;
            set
            {
                SetProperty(ref description, value);
            }
        }

        public string ShortDescription
        {
            get => shortDescription;
            set
            {
                SetProperty(ref shortDescription, value);
            }
        }

        public double Price
        {
            get => price;
            set
            {
                SetProperty(ref price, value);
            }
        }
        #endregion

        #region Functions
        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name) || Name.Length == 0)
                    return "?";

                return Name[0].ToString().ToUpper();
            }
        }

        #endregion
    }
}