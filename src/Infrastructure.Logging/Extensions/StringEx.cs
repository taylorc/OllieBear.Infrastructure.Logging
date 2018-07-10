using System;
using System.Linq;
using System.Net;

namespace Infrastructure.Logging.Extensions
{
    internal static class StringEx
    {
        internal static IPEndPoint ToIpEndPoint(this string host)
        {
            if (string.IsNullOrEmpty(host))
                return null;

            string[] separator = { ",", ";" };

            var hostInfo = host.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            IPAddress ip;

            if (!IPAddress.TryParse(hostInfo[0], out ip))
            {
                ip = Dns.GetHostEntry(hostInfo[0]).AddressList.FirstOrDefault();

                if (ip == null)
                    throw new Exception($"Cannot convert {hostInfo[0]} into IP address.");
            }

            int port;

            if (hostInfo.Length > 1)
            {
                int.TryParse(hostInfo[1], out port);
            }
            else
            {
                throw new ArgumentNullException(nameof(port));
            }

            return new IPEndPoint(ip, port);
        }
    }
}
