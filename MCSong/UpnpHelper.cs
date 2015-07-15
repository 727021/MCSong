using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using NATUPNPLib;


// .NET 4 command line application that lists UPNP mappings on connected routers.
// Uses Windows Native UPnp Support library (NATUPNPLib).
//     v 1.0
//  (9/14/2010) - Alex Soya, Logan Industries, Inc.
//
//

namespace MCSong
{
    internal class UpnpHelper
    {
        UPnPNAT NatMgr;

        public UpnpHelper()
        {
            NatMgr = new UPnPNAT();
        }

        public static string GetLocalIP()
        {
            string IP = null;
            foreach (IPAddress IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (IPAddress.AddressFamily.ToString() == "InterNetwork")
                    IP = IPAddress.ToString();
            }
            return IP;
        }

        private bool DeleteMapNat(int port)
        {

            try
            {
                IStaticPortMappingCollection mappings = NatMgr.StaticPortMappingCollection;
                mappings.Remove(port, "TCP");
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        internal void ListMappings()
        {
            if (NatMgr == null)
            {
                Server.s.Log("Initialization failed creating Windows UPnPNAT interface.");
                return;
            }

            IStaticPortMappingCollection mappings = NatMgr.StaticPortMappingCollection;
            if (mappings == null)
            {
                Server.s.Log("No mappings found. Do you have a uPnP enabled router as your gateway?");
                return;
            }

            if (mappings.Count == 0)
            {
                Server.s.Log("Router does not have any active uPnP mappings.");
            }

            foreach (IStaticPortMapping pm in mappings)
            {
                Server.s.Log("Description:");
                Server.s.Log(pm.Description);
                Server.s.Log(String.Format(" {0}:{1}  --->  {2}:{3} ({4})", pm.ExternalIPAddress, pm.ExternalPort, pm.InternalClient, pm.InternalPort, pm.Protocol));
                Server.s.Log("");
            }

        }

        internal void ClearMappings()
        {
            if (NatMgr == null)
            {
                Server.s.Log("Initialization failed creating Windows UPnPNAT interface.");
                return;
            }

            IStaticPortMappingCollection mappings = NatMgr.StaticPortMappingCollection;
            if (mappings == null)
            {
                Server.s.Log("No mappings found. Do you have a uPnP enabled router as your gateway?");
                return;
            }

            List<IStaticPortMapping> pmsToDelete = new List<IStaticPortMapping>();


            // We need to build our own list as we can not remove from mappings list without altering the list
            // resulting in last entry never being deleted.
            foreach (IStaticPortMapping pm in mappings)
            {
                pmsToDelete.Add(pm);
            }

            foreach (IStaticPortMapping pm in pmsToDelete)
            {
                Server.s.Log(String.Format("Deleting : {0}", pm.Description));
                mappings.Remove(pm.ExternalPort, pm.Protocol);
            }

        }

        internal bool AddMapping(ushort Port, string Protocol, string Description)
        {
            if (NatMgr == null)
            {
                Server.s.Log("Initialization failed creating Windows UPnPNAT interface.");
                return false;
            }

            IStaticPortMappingCollection mappings = NatMgr.StaticPortMappingCollection;
            if (mappings == null)
            {
                Server.s.Log("No mappings found. Do you have a uPnP enabled router as your gateway?");
                return false;
            }

            mappings.Add(Port, Protocol, Port, GetLocalIP(), true, Description);
            return true;
        }
    }
}