using System;

namespace MCSong
{
    class CmdAgree : Command
    {
        public override string name { get { return "agree"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public override void Use(Player p, string message)
        {
            switch (p.agreestring)
            {
                case "gcrules":
                    Server.gcAgreed.Add(p.name);
                    Player.SendMessage(p, "You agreed to the Global Chat rules.");
                    Server.s.Log(p.name + " agreed to the Global Chat rules.");
                    break;
                default:
                    Player.SendMessage(p, "You have nothing to agree to at this time.");
                    break;
            }
            p.agreestring = "";
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/agree - Agree to a set of rules");
        }
    }
}
