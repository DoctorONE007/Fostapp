using System.Collections.Generic;
using System.Net;

namespace ShoppingCarts.Helpers.Interface
{
    public interface IDNSUtils
    {
        List<IPEndPoint> GetDnsServers();
    }
}
