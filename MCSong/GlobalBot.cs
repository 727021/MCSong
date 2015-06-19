using System;
using System.Collections.Generic;
using System.Text;
using Meebey.SmartIrc4net;
using System.Threading;

namespace MCSong
{
    class GlobalBot
    {
        static IrcClient irc = new IrcClient();
        static string server = "irc.esper.net";
        static string channel = "#mcsongglobal";
        static string nick = Server.gcNick;
        static Thread ircThread;

        static string[] names;

        public GlobalBot()
        {
            ircThread = new Thread(new ThreadStart(delegate
            {
                irc.OnConnecting += new EventHandler(OnConnecting);
                irc.OnConnected += new EventHandler(OnConnected);
                irc.OnChannelMessage += new IrcEventHandler(OnChanMessage);
                irc.OnJoin += new JoinEventHandler(OnJoin);
                irc.OnPart += new PartEventHandler(OnPart);
                irc.OnQuit += new QuitEventHandler(OnQuit);
                irc.OnNickChange += new NickChangeEventHandler(OnNickChange);
                irc.OnDisconnected += new EventHandler(OnDisconnected);
                irc.OnQueryMessage += new IrcEventHandler(OnPrivMsg);
                irc.OnNames += new NamesEventHandler(OnNames);
                irc.OnChannelAction += new ActionEventHandler(OnAction);

                try { irc.Connect(server, Server.ircPort); }
                catch (Exception e) { Server.ErrorLog(e); }
            }));
            ircThread.Start();
        }

        void OnConnecting(object sender, EventArgs e)
        {
            Server.s.Log("Connecting to Global Chat");
        }
        void OnConnected(object sender, EventArgs e)
        {
            Server.s.Log("Connected to Global Chat");
            irc.Login(nick, nick, 0, nick);

            if (Server.gcIdentify && Server.gcPassword != string.Empty)
            {
                Server.s.Log("Identifying with Nickserv");
                irc.SendMessage(SendType.Message, "nickserv", "IDENTIFY " + Server.gcPassword);
            }

            Server.s.Log("Joining Global Chat channel");
            irc.RfcJoin(channel);

            irc.Listen();
        }

        void OnNames(object sender, NamesEventArgs e)
        {
            names = e.UserList;
        }
        void OnDisconnected(object sender, EventArgs e)
        {
            Server.s.Log("Disconnected from Global Chat. Trying to reconnect.");
            try { irc.Connect(server, 6667); }
            catch { Server.s.Log("Failed to reconnect to Global Chat"); }
        }

        void OnChanMessage(object sender, IrcEventArgs e)
        {
            string temp = e.Data.Message; string storedNick = e.Data.Nick;

            string allowedchars = "1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./!@#$%^*()_+QWERTYUIOPASDFGHJKL:\"ZXCVBNM<>? ";

            foreach (char ch in temp)
            {
                if (allowedchars.IndexOf(ch) == -1)
                    temp = temp.Replace(ch, '*');
            }

            Server.s.LogGC("[" + storedNick + "]" + temp);
            Player.GlobalMessageGC(Server.gcColor + "[Global][" + storedNick + "]" + temp);
        }

        void OnJoin(object sender, JoinEventArgs e)
        {
            Server.s.LogGC(e.Data.Nick + " has joined Global Chat");
            Player.GlobalMessageGC(Server.gcColor + e.Data.Nick + Server.DefaultColor + " has joined Global Chat");
            irc.RfcNames(channel);
        }
        void OnPart(object sender, PartEventArgs e)
        {
            Server.s.LogGC(e.Data.Nick + " has left Global Chat");
            Player.GlobalMessageGC(Server.gcColor + e.Data.Nick + Server.DefaultColor + " has left Global Chat");
            irc.RfcNames(channel);
        }
        void OnQuit(object sender, QuitEventArgs e)
        {
            irc.RfcNames(channel);
        }

        void OnPrivMsg(object sender, IrcEventArgs e)
        {
            if (Server.devnicks.Contains(e.Data.Nick))
            {
                string dev = Server.devs.ToArray()[Server.devnicks.IndexOf(e.Data.Nick)];// Gets dev's IGN from IRC Nick
                Server.s.LogGC("RECEIVING DEV MESSAGE");// Dev IRC commands will go here
                string cmd = e.Data.Message.Split(' ')[0];
                string msg = "";
                if (e.Data.Message.Split(' ').Length > 1)
                {
                    msg = e.Data.Message.Substring(e.Data.Message.IndexOf(' ')).Trim();
                }
                Server.s.LogGC("[DEV]" + dev + " USED " + cmd.ToLower() + " " + msg);
                switch (cmd.ToLower())
                {
                    case "help":// List GC commands
                        irc.SendMessage(SendType.Message, e.Data.Nick, "Dev GC commands are still in beta. Usable commands are CONSOLE and SAY. HELP is unfinished.");
                        break;
                    case "console":// Use any server command as console
                        if (msg == "") { msg = "console"; goto case "help"; }
                        string com = msg.Split(' ')[0];
                        if (msg.Split(' ').Length > 1) { msg = msg.Substring(msg.IndexOf(' ')).Trim(); }
                        else { msg = ""; }
                        try
                        {
                            if (Command.all.Find(com).consoleUsable)
                            {
                                Command.all.Find(com).Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "/"+com+" can not be used by console.");
                            }
                        }
                        catch
                        {
                            irc.SendMessage(SendType.Message, e.Data.Nick, "Command not found!");
                        }
                        break;
                    case "say":// Send a chat message as irc nick
                        if (msg == "") { msg = "say"; goto case "help"; }
                        Command.all.Find("say").Use(null, Server.gcColor + "[GCPrivate] [&5DEV" + Server.gcColor + "]" + dev + ": &f" + msg);
                        break;
                    default:
                        irc.SendMessage(SendType.Message, e.Data.Nick, "Command not found!");
                        break;
                }
            }
            else
            {
                irc.SendMessage(SendType.Message, e.Data.Nick, "Global Chat commands can only be used by developers");
            }
        }

        void OnNickChange(object sender, NickChangeEventArgs e)
        {
            Player.GlobalMessageGC(Server.gcColor + "[Global] " + e.OldNickname + Server.DefaultColor + " is now known as " + Server.gcColor + e.NewNickname);
            Server.s.LogGC(e.OldNickname + " is now known as " + e.NewNickname);

            irc.RfcNames(channel);
        }

        void OnAction(object sender, ActionEventArgs e)
        {
        }

        /// <summary>
        /// A simple say method for use outside the bot class
        /// </summary>
        /// <param name="msg">what to send</param>
        public static void Say(string msg)
        {
            if (irc != null && irc.IsConnected && Server.gc)
                irc.SendMessage(SendType.Message, channel, msg);
        }
        public static bool IsConnected()
        {
            return irc.IsConnected;
        }

        public static void Reset()
        {
            if (irc.IsConnected)
                irc.Disconnect();
            ircThread = new Thread(new ThreadStart(delegate
            {
                try { irc.Connect(server, Server.ircPort); }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                }
            }));
            ircThread.Start();
        }
        public static string[] GetConnectedUsers()
        {
            return names;
        }
        public static void ShutDown()
        {
            irc.Disconnect();
            ircThread.Abort();
        }
    }
}
