using Android.Net;
using Java.Net;
using ShoppingCarts.Helpers.Interface;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingCarts.Services.ServiceImplementation.MongoService))]
namespace ShoppingCarts.Droid.Utils
{
    public class DNSUtils: IDNSUtils
    {
        public List<IPEndPoint> GetDnsServers()
        {
            var context = Android.App.Application.Context;
            List<IPEndPoint> endPoints = new List<IPEndPoint>();
            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(MainActivity.ConnectivityService);

            Network activeConnection = connectivityManager.ActiveNetwork;
            var linkProperties = connectivityManager.GetLinkProperties(activeConnection);

            foreach (InetAddress currentAddress in linkProperties.DnsServers)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(currentAddress.HostAddress), 53);
                endPoints.Add(endPoint);
            }

            return endPoints;
        }
    }
}