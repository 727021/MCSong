using System;

namespace MCSong
{
    class CmdClearchat : Command
    {
        public override string name { get { return "clearchat"; } }
        public override string[] aliases { get { return new string[] { "cc" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdClearchat() { }

        public override void Use(Player p, string message)
        {
            switch (message)
            {
                case "":
                case "p":
                case "player":
                    if (p == null)
                    {
                        if (!Server.console)
                        {
                            MCSong.Gui.Window.thisWindow.txtLog.Clear();
                            MCSong.Gui.Window.clearChatBuffer();
                        }
                        else Console.Clear();
                    }
                    else
                    {
                        for (int i = 0; i < 100; i++)
                            Clear(p);
                    }
                    Player.SendMessage(p, "Your chat has been cleared.");
                    break;
                case "s":
                case "server":
                    if (p != null && p.group.Permission < Server.opchatperm) { Help(p); return; }
                    foreach (Player pl in Player.players)
                        for (int i = 0; i < 100; i++)
                            Clear(pl);
                    if (p == null)
                    {
                        if (!Server.console)
                        {
                            MCSong.Gui.Window.thisWindow.txtLog.Clear();
                            MCSong.Gui.Window.clearChatBuffer();
                        }
                        else Console.Clear();
                    }
                    Server.s.Log("Server chat has been cleared by " + ((p == null) ? "Console" : p.color + p.name));
                    Player.GlobalMessage("Server chat has been cleared by " + ((p == null) ? "Console" : p.color + p.name));
                    break;
                default:
                    Help(p);
                    return;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clearchat - Clears your chat");
            if (p == null || (p != null && p.group.Permission >= Server.opchatperm))
            {
                Player.SendMessage(p, "/clearchat server - Clears the server's chat");
            }
        }

        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        private byte[] Format(string msg, int size)
        {
            byte[] b = new byte[size];
            b = enc.GetBytes(msg.PadRight(size).Substring(0, size));
            return b;
        }

        private void Clear(Player p)
        {
            byte[] buffer = new byte[65];
            Format(" ", 64).CopyTo(buffer, 1);
            p.SendRaw(13, buffer);
        }
    }
}
