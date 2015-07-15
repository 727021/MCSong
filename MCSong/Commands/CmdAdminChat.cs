using System;

namespace MCSong
{
    public class CmdAdminChat : Command
    {
        public override string name { get { return "adminchat"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            p.adminchat = !p.adminchat;
            if (p.adminchat) Player.SendMessage(p, "All messages will now be sent to Admins only");
            else Player.SendMessage(p, "Admin chat turned off");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/adminchat - Makes all messages sent go to Admins by default");
        }
    }
}