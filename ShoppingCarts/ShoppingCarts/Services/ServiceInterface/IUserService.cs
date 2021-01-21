using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using System.Threading.Tasks;

namespace ShoppingCarts.Services.ServiceInterface
{
    public interface IUserService
    {
        Task<TryResult<User>> LoginAsync(string phone, string passwordHash);
        Task<TryResult<User>> RegisterAsync(string firstName, string middleName, string lastName, string email, string phone, string passwordHash);
        TryResult<User> GetCurrentUser();
    }
}
