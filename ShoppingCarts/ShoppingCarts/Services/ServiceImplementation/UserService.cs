using MongoDB.Bson;
using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using ShoppingCarts.Storage;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingCarts.Services.ServiceImplementation.UserService))]

namespace ShoppingCarts.Services.ServiceImplementation
{
    public class UserService : IUserService
    {
        #region Fields
        private static User currentUser;
        private IUserStorage userStorage;
        #endregion

        public UserService()
        {
            userStorage = DependencyService.Get<IUserStorage>();
        }

        public TryResult<User> GetCurrentUser()
        {
            return currentUser != null ?
                new TryResult<User>(currentUser) :
                new TryResult<User>(new Exception("Пользователь не вошел в систему"));
        }

        public async Task<TryResult<User>> LoginAsync(string phone, string passwordHash)
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(passwordHash))
                return new TryResult<User>(new Exception("Заполните все необходимые поля"));

            var result = await userStorage.GetUserByPhoneAndPassAsync(phone, passwordHash);

            if (!result.IsFaulted)
            {
                if (result.Value == null)
                    return new TryResult<User>(new Exception("Пользователь с такими логином/паролем не найден"));
                currentUser = result.Value;
            }

            return result;
        }

        public async Task<TryResult<User>> RegisterAsync(string firstName, string middleName, string lastName, string email, string phone, string passwordHash)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordHash) || string.IsNullOrEmpty(phone))
                return new TryResult<User>(new Exception("Заполните все необходимые поля"));

            currentUser = new User
            {                
                Id = ObjectId.GenerateNewId(),
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                PasswordHash = passwordHash
            };

            return await userStorage.CreateUserAsync(currentUser);
        }
    }
}
