using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    internal class RemoteServer
    {
        public struct User
        {
            public string name;
            public string pass;
        }

        public static List<User> remotes = new List<User>();
        public static ushort port = 4700;
        public static string rpass = "";
        public static byte version = 1;
        public static Socket listen;

        public static void Start()
        {
            try
            {
                Server.s.Log("Creating listening socket on port " + port + "for Remote Console...");
                if (Setup())
                {
                    Server.s.Log("Done.");
                }
                else
                {
                    Server.s.Log("Could not create socket connection for Remote Console.");
                    return;
                }
            }
            catch
            {

            }
        }

        public static bool Setup()
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);
                listen = new Socket(endpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listen.Bind(endpoint);
                listen.Listen((int)SocketOptionName.MaxConnections);

                listen.BeginAccept(new AsyncCallback(Accept), null);
                return true;
            }
            catch (Exception e) { Server.ErrorLog(e); return false; }
        }

        static void Accept(IAsyncResult result)
        {
            if (Server.shuttingDown == false)
            {
                // found information: http://www.codeguru.com/csharp/csharp/cs_network/sockets/article.php/c7695
                // -Descention
                Remote p = null;
                try
                {
                    p = new Remote(listen.EndAccept(result));
                    listen.BeginAccept(new AsyncCallback(Accept), null);
                }
                catch (SocketException)
                {
                    if (p != null)
                        p.Disconnect();
                }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                    if (p != null)
                        p.Disconnect();
                }
            }
        }
    }
}
