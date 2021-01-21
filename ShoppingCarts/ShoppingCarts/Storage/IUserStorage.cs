using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCarts.Storage
{
    public interface IUserStorage
    {
        Task<TryResult<User>> GetUserByPhoneAndPassAsync(string phone, string password, CancellationToken cancellationToken = default);
        Task<TryResult<User>> CreateUserAsync(User user, CancellationToken cancellationToken = default);
    }
}
