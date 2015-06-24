using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    class CmdGCRules : Command
    {
        public override string name { get { return "gcrules"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "MCSong Global Chat Rules:");
            Player.SendMessage(p, "- - - - - - - - - - - - - -");
            Player.SendMessage(p, "1. Be respectful");
            Player.SendMessage(p, "2. Don't spam");
            Player.SendMessage(p, "3. Don't use foul language");
            Player.SendMessage(p, "4. Obey MCSong staff");
            Player.SendMessage(p, "5. Don't advertise");
            Player.SendMessage(p, "- - - - - - - - - - - - - -");
            Player.SendMessage(p, "Use &a/agree " + Server.DefaultColor + " to agree to the Global Chat rules.");
            p.agreestring = "gcrules";
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/gcrules - Read the Global Chat rules");
        }
    }
}
