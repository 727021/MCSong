using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    internal class Remote
    {
        public static UTF8Encoding enc = new UTF8Encoding();
        public static List<Remote> remotes = new List<Remote>();
        
        public string name = "";
        public string username = "";
        public string pass = "";
        public string ip = "";
        public bool loggedIn = false;
        public bool disconnected = false;
        public Socket socket { get; private set; }
        byte[] tempbuffer = new byte[0xFF];
        byte[] buffer = new byte[0];

        public Remote(Socket s)
        {
            try
            {
                loggedIn = false;
                tempbuffer = new byte[0xFF];
                buffer = new byte[0];
                socket = s;
                ip = socket.RemoteEndPoint.ToString().Split(':')[0];
                Server.s.Log("Remote Console at " + ip + "connecting...");
                socket.BeginReceive(tempbuffer, 0, tempbuffer.Length, SocketFlags.None, new AsyncCallback(Receive), this);
            }
            catch (Exception e)
            {
                Disconnect("Login failed!"); Server.ErrorLog(e);
            }
        }

        static void Receive(IAsyncResult result)
        {
            //    Server.s.Log(result.AsyncState.ToString());
            Remote p = (Remote)result.AsyncState;
            if (p.disconnected)
                return;
            try
            {
                int length = p.socket.EndReceive(result);
                if (length == 0) { p.Disconnect(); return; }

                byte[] b = new byte[p.buffer.Length + length];
                Buffer.BlockCopy(p.buffer, 0, b, 0, p.buffer.Length);
                Buffer.BlockCopy(p.tempbuffer, 0, b, p.buffer.Length, length);

                p.buffer = p.HandleMessage(b);
                p.socket.BeginReceive(p.tempbuffer, 0, p.tempbuffer.Length, SocketFlags.None, new AsyncCallback(Receive), p);
            }
            catch (SocketException)
            {
                p.Disconnect("Error!");
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                if (p != null) p.Disconnect("Error!");
                Server.s.Log("Attempting to restart socket for Remote Console...");
                RemoteServer.listen = null;
                if (RemoteServer.Setup()) { Server.s.Log("Listening socket on " + RemoteServer.port + "for Remote Console restarted."); }
                else { Server.s.Log("Failed to restart listening socket for Remote Console."); }
            }
        }

        byte[] HandleMessage(byte[] buffer)
        {
            try
            {
                int length = 0; byte msg = buffer[0];
                // Get the length of the message by checking the first byte
                switch (msg)
                {
                    case 0:
                        length = 257;
                        break;
                    case 2:
                        if (!loggedIn) goto default;
                        length = 64;
                        break;
                    default:
                        Disconnect("Unhandled message id \"" + msg + "\"!");
                        return new byte[0];
                }
                if (buffer.Length > length)
                {
                    byte[] message = new byte[length];
                    Buffer.BlockCopy(buffer, 1, message, 0, length);

                    byte[] tempbuffer = new byte[buffer.Length - length - 1];
                    Buffer.BlockCopy(buffer, length + 1, tempbuffer, 0, buffer.Length - length - 1);

                    buffer = tempbuffer;

                    // Thread thread = null; 
                    switch (msg)
                    {
                        case 0:
                            HandleLogin(message);
                            break;
                        case 2:
                            if (!loggedIn) break;
                            HandleChat(message);
                            break;
                    }
                    //thread.Start((object)message);
                    if (buffer.Length > 0)
                        buffer = HandleMessage(buffer);
                    else
                        return new byte[0];
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
            }
            return buffer;
        }

        public void HandleLogin(byte[] message)
        {

        }

        public void HandleChat(byte[] message)
        {

        }


        public void Disconnect(string reason = "")
        {
            try
            {
                disconnected = true;
                //SendKick(reason);
                if (loggedIn)
                {
                    Server.s.Log("Remote Console user " + name + " disconnected.");
                    remotes.Remove(this);
                }
                else
                {
                    Server.s.Log("Remote Console at " + ip + " disconnected.");
                }
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                }
                //RemoteServer.RemoteListUpdate();
            }
            catch (Exception e) { Server.ErrorLog(e); }
        }
    }
}
