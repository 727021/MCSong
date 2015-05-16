using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCSong
{
    public class CmdCrashServer : Command
    {
        public override string name { get { return "crashserver"; } }
        public override string[] aliases { get { return new string[] { "crash" }; } }
        public override CommandType type { get { return CommandType.Moderation; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdCrashServer() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            Player.GlobalMessageOps(p.color + Server.DefaultColor + " used &b/crashserver");
            p.Kick("Server crash! Error code 0x0005A4");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/crashserver - Crash the server with a generic error");
        }
    }

}
